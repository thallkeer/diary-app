using DiaryApp.Infrastructure.ServiceInterfaces;
using Quartz;
using System.Threading.Tasks;

namespace DiaryApp.Infrastructure.Quartz.Jobs
{
    public class NotifyUserJob : IJob
    {
        public static readonly string JobDataKey = "NOTIFICATION_ID";

        private readonly INotificationService _notificationService;

        public NotifyUserJob(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var cancellationToken = context.CancellationToken;
            var notificationId = GetNotificationId(context);
            await _notificationService.NotifyAsync(notificationId, cancellationToken);
        }

        private static int GetNotificationId(IJobExecutionContext context)
        {
            JobDataMap jobDataMap = context.JobDetail.JobDataMap;

            return jobDataMap.GetIntValue(JobDataKey);
        }
    }
}
