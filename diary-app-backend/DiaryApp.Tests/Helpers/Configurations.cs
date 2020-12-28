using AutoMapper;
using DiaryApp.API.Extensions.ConfigureServices;
using DiaryApp.Core;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.Tests.Helpers
{
    internal static class Configurations
    {
        public static ApplicationContext GetDbContext()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseInMemoryDatabase(databaseName: "DiaryAppDb"));

            services.AddSingleton(new JwtTokenConfig
            {
                Secret = "testsecret",
                AccessTokenExpiration = 30
            });

            services.AddApplicationServices();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            return SeedData.InitializeContextWithSeedData(services);
        }       

        public static IConfigurationProvider GetMapperProvider()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IConfigurationProvider>();
        }

        public static IMapper GetMapper()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IMapper>();
        }
    }
}