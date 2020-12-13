using AutoMapper;
using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DiaryApp.Tests.Helpers
{
    internal static class Configurations
    {
        //private static string ConnectionString { get; } = GetConfigurations().GetConnectionString("DefaultConnection");

        //private static IConfiguration GetConfigurations()
        //{
        //    return new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //}

        public static ApplicationContext GetDbContext()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase(databaseName: "DiaryAppDb"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            var pages = new List<MainPage>
            {
                new MainPage { Id=1, UserID = 2, Year = 2020, Month = 8 },
                new MainPage { Id=2, UserID = 1, Year = 2020, Month = 10 },
                new MainPage { Id=3, UserID = 3, Year = 2020, Month = 4 },
                new MainPage { Id=4, UserID = 4, Year = 2020, Month = 1 }
            };

            pages.ForEach(p => p.CreateAreas());

            context.MainPages.AddRange(pages);

            context.SaveChangesAsync();

            return context;
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
