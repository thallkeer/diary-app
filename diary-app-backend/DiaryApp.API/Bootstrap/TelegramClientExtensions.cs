using DiaryApp.API.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;
using Telegram.Bot;

namespace DiaryApp.API.Bootstrap
{
    public static class TelegramClientExtensions
    {
        public static IServiceCollection AddTelegramClient(this IServiceCollection services, AppSettings appSettings)
        {
            if (appSettings == null)
                throw new ArgumentNullException(nameof(appSettings));
            string botToken = appSettings.TelegramBotToken;
            if (string.IsNullOrEmpty(botToken))
                throw new ArgumentException("Settings must contain telegram bot token!", nameof(appSettings));
            
            return services.AddScoped<ITelegramBotClient>(provider =>
            {
                var botClient = new TelegramBotClient(botToken);
                return botClient;
            });
        }
    }
}
