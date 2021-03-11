using DiaryApp.Core;
using DiaryApp.Services.Bootstrap;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiaryApp.IntegrationTests.Helpers
{
    internal static class Configurations
    {
        public static ApplicationContext GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>(options => options
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase(databaseName: $"diaryAppDb-{Guid.NewGuid()}")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

            services.AddDataServices();

            services.AddAutoMapper(typeof(API.Startup).Assembly);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<ApplicationContext>();
        }
    }
}