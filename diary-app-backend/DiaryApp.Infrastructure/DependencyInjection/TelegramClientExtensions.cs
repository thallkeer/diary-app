using Microsoft.Extensions.DependencyInjection;
using System;
using Telegram.Bot;

namespace DiaryApp.Infrastructure.DependencyInjection
{
    public static class TelegramClientExtensions
    {
        public static IServiceCollection AddTelegramClient(this IServiceCollection services, string telegramBotToken)
        {
            if (string.IsNullOrEmpty(telegramBotToken))
                throw new ArgumentException("Settings must contain telegram bot token!", nameof(telegramBotToken));
            
            return services.AddScoped<ITelegramBotClient>(provider =>
            {
                var botClient = new TelegramBotClient(telegramBotToken);
                return botClient;
            });
        }
    }
}
