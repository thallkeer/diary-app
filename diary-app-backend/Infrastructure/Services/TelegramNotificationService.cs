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

namespace DiaryApp.Services.Services
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
            _logger.LogDebug($"Sending scheduled Message ID {notificationId}");

            var notification = await _eventNotificationService.GetByIdAsync(notificationId);

            try
            {
                var chatId = new ChatId(notification.User.TelegramId.Value);
                var msg = await _botClient.SendTextMessageAsync(chatId, notification.Subject, cancellationToken: cancellationToken);

                _logger.LogDebug($"Sended scheduled notification {notificationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending notification {notificationId}");
            }
        }
    }
}
