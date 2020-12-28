using AutoFixture;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Tests.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Tests.Helpers
{
    public static class SeedData
    {
        internal static ApplicationContext InitializeContextWithSeedData(ServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            var userService = serviceProvider.GetRequiredService<IUserService>();

            var fixture = new Fixture();

            var users = fixture.Build<UserDto>().Without(u => u.Id).CreateMany(6).ToList();

            var password = fixture.Create<string>();

            users.ForEach(u => userService.RegisterAsync(u, password));

            var mainPageService = serviceProvider.GetRequiredService<IMainPageService>();
            var monthPageService = serviceProvider.GetRequiredService<IMonthPageService>();

            foreach (var user in context.Users)
            {
                mainPageService.CreateAsync(user.Id, fixture.CreateYear(), fixture.CreateMonth());
                monthPageService.CreateAsync(user.Id, fixture.CreateYear(), fixture.CreateMonth());
            }

            return context;
        }
    }
}
