﻿using DiaryApp.Infrastructure.Quartz.JobFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace DiaryApp.Infrastructure.DependencyInjection
{
    public static class QuartzExtensions
    {
        public static IApplicationBuilder UseQuartz(this IApplicationBuilder app)
        {
            var scheduler = app.ApplicationServices.GetService<IScheduler>();

            scheduler.Start().GetAwaiter().GetResult();

            return app;
        }

        public static IServiceCollection AddQuartzScheduler(this IServiceCollection services)
        {
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

                scheduler.JobFactory = provider.GetService<IJobFactory>();

                return scheduler;
            });

            return services;
        }
    }
}
