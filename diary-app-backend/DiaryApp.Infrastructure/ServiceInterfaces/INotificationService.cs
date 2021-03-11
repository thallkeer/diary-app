using System.Threading;
using System.Threading.Tasks;

namespace DiaryApp.Infrastructure.ServiceInterfaces
{
    /// <summary>
    /// Interface for services, that provide possibilities to send notifications for users
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends notification with given id to corresponding source
        /// </summary>
        /// <param name="notificationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task NotifyAsync(int notificationId, CancellationToken cancellationToken);
    }
}