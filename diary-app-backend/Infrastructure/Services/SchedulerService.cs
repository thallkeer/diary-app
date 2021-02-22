using DiaryApp.Infrastructure.ServiceInterfaces;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Infrastructure.Quartz.Jobs;

namespace DiaryApp.Infrastructure.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IScheduler _scheduler;

        public SchedulerService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task ScheduleMessageAsync(NotificationDto notification, CancellationToken cancellationToken = default)
        {
            IJobDetail job = JobBuilder.Create<NotifyUserJob>()
                                       .UsingJobData(NotifyUserJob.JobDataKey, notification.Id)
                                       .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(Guid.NewGuid().ToString(), "telegramNotifications")
                .StartAt(notification.NotificationDate)
                .Build();

            await _scheduler.ScheduleJob(job, trigger, cancellationToken);
        }
    }
}
