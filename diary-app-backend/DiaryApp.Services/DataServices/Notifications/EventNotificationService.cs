using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.ListItems;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataServices.Notifications
{
    public class EventNotificationService : CrudService<NotificationDto, Notification>, IEventNotificationService
    {
        private readonly IUserService _userService;
        private readonly IEventItemService _eventItemService;

        public EventNotificationService(ApplicationContext context, IMapper mapper,
            IUserService userService, IEventItemService eventItemService)
            : base(context, mapper)
        {
            _userService = userService;
            _eventItemService = eventItemService;
        }

        public async Task<List<NotificationDto>> CreateNotificationsIfNecessary(int userId, int eventId)
        {
            var user = await _userService.FirstOrDefaultAsync<UserWithSettingsDto>(u => u.Id == userId);

            List<NotificationDto> createdNotifications = new();

            if (!IsUserAllowsNotifications(user)) return createdNotifications;
            
            var notificationSettings = user.Settings.NotificationSettings;
            var createdEvent = await _eventItemService.GetByIdAsync(eventId);

            async void TryCreateAndAddToResultList(DateTime dateToNotify, bool forDayBefore)
            {
                var dateWithTimeToNotify = CombineDateAndTimeToNotify(dateToNotify, notificationSettings.NotifyAt);
                var notification = await CreateNotificationAsync(createdEvent, user, dateWithTimeToNotify, forDayBefore);
                if (notification != null)
                    createdNotifications.Add(notification);
            }

            var eventDate = createdEvent.Date.Date;
            if (notificationSettings.NotifyDayBefore)
            {
                TryCreateAndAddToResultList(eventDate.AddDays(-1), true);
            }
            TryCreateAndAddToResultList(eventDate, false);

            return createdNotifications;
        }

        private async Task<NotificationDto> CreateNotificationAsync(EventItemDto eventItem, UserDto user, DateTime dateToNotify, bool forDayBefore)
        {
            if (dateToNotify <= DateTime.Today || !user.TelegramId.HasValue) return null;
            var eventDate = eventItem.Date.Date;

            var day = forDayBefore ? "завтра" : "сегодня";

            var notification = new NotificationDto
            {
                Subject = $"Напоминание: {day} {eventDate} {eventItem.Subject}",
                NotificationDate = dateToNotify,
                UserId = user.Id,
                UserTelegramId = user.TelegramId.Value,
                EventId = eventItem.Id
            };

            var notificationId = await CreateAsync(notification);
            return await GetByIdAsync(notificationId);
        }

        private static bool IsUserAllowsNotifications(UserWithSettingsDto user)
        {
            if (!user.TelegramId.HasValue) return false;
            var notificationSettings = user.Settings?.NotificationSettings;
            return notificationSettings != null && notificationSettings.IsActivated;
        }

        private static DateTime CombineDateAndTimeToNotify(DateTime dateToNotify, TimeSpan timeToNotify) =>
            new(dateToNotify.Year, dateToNotify.Month, dateToNotify.Day, timeToNotify.Hours, timeToNotify.Minutes, timeToNotify.Seconds);
    }
}