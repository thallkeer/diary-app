using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Infrastructure.ServiceInterfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using DiaryApp.Services.DTO.Notifications;

namespace DiaryApp.Infrastructure.Services
{
    public class TelegramNotificationService : INotificationService
    {
        private readonly ILogger<TelegramNotificationService> _logger;
        private readonly ITelegramBotClient _botClient;
        private readonly ICrudService<NotificationDto, Notification> _eventNotificationService;

        public TelegramNotificationService(
            ITelegramBotClient botClient,
            ICrudService<NotificationDto, Notification> eventNotificationService, 
            ILogger<TelegramNotificationService> logger)
        {
            _botClient = botClient;
            _eventNotificationService = eventNotificationService;
            _logger = logger;
        }

        public async Task NotifyAsync(int notificationId, CancellationToken cancellationToken)
        {
            var notification = await _eventNotificationService.GetByIdAsync(notificationId);
            if (notification == null)
            {
                _logger.LogError($"Scheduled notification with Id {notificationId} is not found in database");
                return;
            }

            try
            {
                var chatId = new ChatId(notification.UserTelegramId);
                var msg = await _botClient.SendTextMessageAsync(chatId, notification.Subject, cancellationToken: cancellationToken);
                _logger.LogDebug($"Sended scheduled notification {notificationId}");

                await _eventNotificationService.DeleteAsync(notificationId);
                _logger.LogDebug($"Deleted scheduled notification {notificationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending notification {notificationId}");
                throw;
            }
        }
    }
}
