using System;
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
using System.Text;
using DiaryApp.API.Extensions;
using System.Threading.Tasks;
using DiaryApp.API.Extensions.ConfigureServices;
using DiaryApp.Core.Bootstrap;

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

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "client/build");

            services.AddAutoMapper(typeof(Startup));

            if (env.IsDevelopment())
            {
                services.AddSqlServerContext(Configuration);
            }
            else
            {
                services.AddPostgresContext(Configuration);
            }

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.ConfigureJwtAuthentication(key);

            services.AddApplicationServices();

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ApplicationContext appContext)
        {
            appContext.Database.Migrate();

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
                app.ConfigureExceptionHandler(logger);
                app.UseHsts();
            }

            app.UseCors(builder =>
                                    builder.AllowAnyOrigin()
                                           .AllowAnyHeader()
                                           .AllowAnyMethod());

           
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //на сервере писать через относительный путь
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiaryApp API V1");
            });

            app.UseRouting();

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
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });

            app.Run(async (context) =>
            {
                await Task.Run(() => logger.LogInformation($"{DateTime.Now} Processing request {context.Request.Path}"));
                //await context.Response.WriteAsync("TEST");
            });
        }
    }
}