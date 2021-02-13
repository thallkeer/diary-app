using DiaryApp.Models.DTO.Notifications;
using DiaryApp.Services.Quartz.Jobs;
using DiaryApp.Services.ServiceInterfaces;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryApp.Services.Jobs
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
                //.WithSimpleSchedule(x => x.WithRepeatCount(0))
                .Build();

            await _scheduler.ScheduleJob(job, trigger, cancellationToken);
        }
    }
}
