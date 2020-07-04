﻿using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.ServiceInterfaces;
using DiaryApp.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace DiaryApp.API
{
    public class Startup
    {
        readonly IWebHostEnvironment env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllers();

            services.AddCors();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "client/build");

            services.AddAutoMapper(typeof(Startup));

            if (env.IsDevelopment())
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseLazyLoadingProxies()
                           .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                //.AddEntityFrameworkNpgsql()
                services
                        .AddDbContext<ApplicationContext>(options =>
                            options.UseLazyLoadingProxies()
                                   .UseNpgsql(Configuration.GetConnectionString("ProdConnection")));
            }

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ICommonListService, CommonListService>();
            services.AddScoped<IMainPageService, MainPageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMonthPageService, MonthPageService>();
            services.AddScoped<IHabitTrackerService, HabitTrackerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger<FileLogger>();

            //app.Use(async (ctx, next) =>
            //{
            //    await next();
            //    if (ctx.Response.StatusCode == 204)
            //    {
            //        ctx.Response.ContentLength = 0;
            //    }
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature.Error;

                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
                ///app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseCors(builder =>
                                    builder.AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod());

            ///TODO: deal with errors and codes
            //app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

            //app.Map("/error", ap => ap.Run(async context =>
            //{
            //    string message = $"{DateTime.Now} {context.Request.Path} {context.Request.QueryString}" +
            //        $" Error: {context.Request.Path} {context.Request.Query["code"]}";
            //    logger.LogError(message);
            //    await context.Response.WriteAsync(message);
            //}));

            //SampleData.Initialize(app.ApplicationServices);



            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc();
            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    ///TODO: get source path from config
                    spa.Options.SourcePath = @"E:\repos\diaryApp\diary-app-frontend";
                    //spa.UseReactDevelopmentServer(npmScript: "start");
                }
                else
                {
                    spa.Options.SourcePath = Path.Join(env.ContentRootPath, "client");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });

            //app.Run(async (context) =>
            //{
            //    logger.LogInformation($"{DateTime.Now} Processing request {context.Request.Path}");
            //    //await context.Response.WriteAsync("TEST");
            //});
        }
    }
}