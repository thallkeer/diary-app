using AutoMapper;
using DiaryApp.API.Extensions.ConfigureServices;
using DiaryApp.Core;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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

            services.AddApplicationServices();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            var userService = serviceProvider.GetRequiredService<IUserService>();

            for (int i = 0; i < 12; i++)
            {
                userService.CreateAsync(new Core.DTO.UserDto($"TestUser{i+1}"), $"testuser{i+1}-password");
            }

            var mainPageService = serviceProvider.GetRequiredService<IMainPageService>();
            var monthPageService = serviceProvider.GetRequiredService<IMonthPageService>();

            foreach (var user in context.Users)
            {
                mainPageService.CreateAsync(user.Id, 2020, user.Id);
                mainPageService.CreateAsync(user.Id, 2020, user.Id == 1 ? 12 : user.Id - 1);

                monthPageService.CreateAsync(user.Id, 2020, user.Id);
                if (user.Id != 12)
                    monthPageService.CreateAsync(user.Id, 2020, user.Id + 1);
                monthPageService.CreateAsync(user.Id, 2020, user.Id == 1 ? 12 : user.Id - 1);
            }

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
