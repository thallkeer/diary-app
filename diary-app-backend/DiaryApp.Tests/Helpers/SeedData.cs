using AutoFixture;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DiaryApp.Tests.Helpers
{
    public static class SeedData
    {
        internal static ApplicationContext InitializeContextWithSeedData(ServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<ApplicationContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var userService = serviceProvider.GetRequiredService<IUserService>();

            var fixture = new Fixture();

            var users = fixture.Build<UserDto>().Without(u => u.Id).CreateMany(6).ToList();

            var password = fixture.Create<string>();

            users.ForEach(u => userService.RegisterAsync(u, password));

            //var mainPageService = serviceProvider.GetRequiredService<IMainPageService>();
            //var monthPageService = serviceProvider.GetRequiredService<IMonthPageService>();

            //foreach (var user in context.users)
            //{
            //    mainpageservice.createasync(user.id, fixture.createyear(), fixture.createmonth());
            //    monthpageservice.createasync(user.id, fixture.createyear(), fixture.createmonth());
            //}

            return context;
        }
    }
}
