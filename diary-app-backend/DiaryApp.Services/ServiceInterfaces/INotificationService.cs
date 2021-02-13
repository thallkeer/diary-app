using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiaryApp.Services.ServiceInterfaces
{
    /// <summary>
    /// Interface for services, that provide possibilities to send notifications for users
    /// </summary>
    public interface INotificationService
    {
        Task NotifyAsync(int notificationId, CancellationToken cancellationToken);
    }
}
