using DiaryApp.Services.DTO.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IEventNotificationService
    {
        /// <summary>
        /// Creates one or more notifications for given event
        /// if user settings allows it
        /// </summary>
        /// <param name="userId">Identifier of the user to be notified of the event</param>
        /// <param name="eventId">Identifier of event</param>
        /// <returns>List with created notifications or empty list if notifications weren't created</returns>
        Task<List<NotificationDto>> CreateNotificationsIfNecessary(int userId, int eventId);
    }
}
