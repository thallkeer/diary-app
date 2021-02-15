using DiaryApp.Services.DTO.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryApp.Infrastructure.ServiceInterfaces
{
    public interface ISchedulerService
    {
        Task ScheduleMessageAsync(NotificationDto notification, CancellationToken cancellationToken = default);
    }
}
