using AutoFixture;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Infrastructure.Security;
using DiaryApp.Services.Bootstrap;
using DiaryApp.Tests.Customizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace DiaryApp.Tests.Helpers
{
    internal static class Configurations
    {
        public static ApplicationContext GetServiceProvider()
        {
            System.Console.WriteLine("GetServiceProvider", new StackTrace(1, true));

            var services = new ServiceCollection();            

            services.AddDbContext<ApplicationContext>(options => options
                    .UseLazyLoadingProxies()
                    //.UseInMemoryDatabase(databaseName: $"diaryAppDb-{Guid.NewGuid()}")
                    //.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                    .UseSqlServer("Server=DESKTOP-UR40SF3; Database=diaryapptest; Trusted_Connection=True; MultipleActiveResultSets=true")
                    , ServiceLifetime.Transient);

            services.AddSingleton(new JwtTokenConfig
            {
                Secret = "testsecret",
                AccessTokenExpiration = 30
            });

            services.AddDataServices();

            services.AddAutoMapper(typeof(API.Startup).Assembly);           

            SeedData.InitializeContextWithSeedData(services);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<ApplicationContext>();
        }

        public static IFixture GetFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //fixture.Customize(new PageAreaCustomization());
            fixture.Customize(new PageCustomization());
            return fixture;
        }

        public static IConfigurationProvider GetMapperProvider()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IConfigurationProvider>();
        }
    }
}