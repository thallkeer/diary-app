using AutoMapper;
using DiaryApp.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using DiaryApp.API.Extensions;
using DiaryApp.API.Extensions.ConfigureServices;
using DiaryApp.Core.Bootstrap;
using DiaryApp.Data.Services.Users;
using System;
using Microsoft.AspNetCore.HttpOverrides;

namespace DiaryApp.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;
        readonly string DiaryAppPolicy = nameof(DiaryAppPolicy);

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            env = webHostEnvironment;
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

            services.AddCors(options =>
            {
                options.AddPolicy(DiaryAppPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5001", "http://localhost:3000");
                        builder.WithMethods("GET", "POST", "PUT", "DELETE");
                        builder.WithExposedHeaders("www-authenticate", "Access-Token");
                        builder.AllowAnyHeader();
                        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
                    });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "client/build");

            services.AddAutoMapper(typeof(Startup));

            string connectionString = Configuration.GetConnectionString(env.IsDevelopment() ? "DefaultConnection" : "ProdConnection");
            services.AddPostgresContext(connectionString);

            // configure strongly typed settings objects
            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);

            services.ConfigureJwtAuthentication(jwtTokenConfig);

            services.AddApplicationServices();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationContext appContext)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger<FileLogger>();

            appContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler();

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiaryApp API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseCors(DiaryAppPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    var spaPathSection = Configuration.GetSection("SpaSourcePath");
                    spa.Options.SourcePath = spaPathSection.Value;
                    //spa.UseReactDevelopmentServer(npmScript: "start");
                }
                else
                {
                    spa.Options.SourcePath = Path.Join(env.ContentRootPath, "client");
                    //spa.UseProxyToSpaDevelopmentServer("https://localhost:5001");
                }
            });
        }
    }
}