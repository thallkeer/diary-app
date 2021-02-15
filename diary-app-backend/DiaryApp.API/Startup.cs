using DiaryApp.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using DiaryApp.Core.Bootstrap;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using DiaryApp.API.Filters;
using DiaryApp.API.Settings;
using DiaryApp.Services.Bootstrap;
using DiaryApp.API.Middleware;
using DiaryApp.Services.Security;
using DiaryApp.Infrastructure.Security;
using DiaryApp.Infrastructure.ServiceInterfaces;
using DiaryApp.Services.Jobs;
using DiaryApp.Services.Services;
using DiaryApp.Infrastructure.DependencyInjection;
using Infrastructure.DependencyInjection;
using System.Reflection;

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
            var appSettings = Configuration.GetSection("appSettings").Get<AppSettings>();
            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

            services.AddCors(options =>
            {
                options.AddPolicy(DiaryAppPolicy,
                    builder =>
                    {
                        builder.WithOrigins(appSettings.Origins);
                        builder.WithMethods("GET", "POST", "PUT", "DELETE");
                        builder.WithExposedHeaders("www-authenticate", "Access-Token");
                        builder.AllowAnyHeader();
                        builder.SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
                    });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllers();

            string connectionString = Configuration.GetConnectionString(env.IsDevelopment() ? "DefaultConnection" : "ProdConnection");

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }).AddJwtAuthentication(jwtTokenConfig);

            services.AddAutoMapper(typeof(Startup))
                    .AddSingleton(appSettings)
                    .AddSingleton(jwtTokenConfig)
                    .AddScoped<IJwtAuthManager,JwtAuthManager>()
                    .AddScoped<ModelValidationAttribute>()
                    .AddQuartzScheduler()
                    .AddPostgresContext(connectionString)
                    .AddGithubService(appSettings.GithubToken)
                    .AddDataServices()
                    .AddTelegramClient(appSettings.TelegramBotToken)
                    .AddTransient<ISchedulerService, SchedulerService>()        
                    .AddScoped<INotificationService, TelegramNotificationService>()
                    .AddMvc(opt => opt.EnableEndpointRouting = false);

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "client/build");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext appContext)
        {
            appContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

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
                }
                else
                {
                    spa.Options.SourcePath = Path.Join(env.ContentRootPath, "client");
                }
            });

            app.UseQuartz();
        }
    }
}