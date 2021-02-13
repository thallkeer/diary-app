using DiaryApp.Models.DTO.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryApp.Services.ServiceInterfaces
{
    public interface ISchedulerService
    {
        Task ScheduleMessageAsync(NotificationDto notification, CancellationToken cancellationToken = default);
    }
}
