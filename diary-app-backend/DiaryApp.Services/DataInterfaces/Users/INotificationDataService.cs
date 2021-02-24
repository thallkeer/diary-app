using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DTO.Notifications;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface INotificationDataService : ICrudService<NotificationDto, Notification>
    {
        Task<NotificationDto> TryCreateNotificationAsync(int userId, int eventId);
    }
}
