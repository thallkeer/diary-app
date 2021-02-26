using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Lists;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataServices.Notifications
{
    public class NotificationDataService : CrudService<NotificationDto, Notification>, INotificationDataService
    {
        private readonly IUserService _userService;
        private readonly IEventItemService _eventItemService;

        public NotificationDataService(ApplicationContext context, IMapper mapper,
            IUserService userService, IEventItemService eventItemService)
            : base(context, mapper)
        {
            _userService = userService;
            _eventItemService = eventItemService;
        }

        public async Task<List<NotificationDto>> CreateNotificationsIfNecessary(int userId, int eventId)
        {
            var createdEvent = await _eventItemService.GetByIdAsync(eventId);
            var user = await _userService.FirstOrDefaultAsync<UserWithSettingsDto>(u => u.Id == userId);

            List<NotificationDto> createdNotifications = new();

            if (user.TelegramId.HasValue)
            {
                var notificationSettings = user.Settings?.NotificationSettings;
                if (notificationSettings != null && notificationSettings.IsActivated)
                {
                    async void TryCreateAndAddToResultList(DateTime dateToNotify, bool forDayBefore)
                    {
                        var notification = await CreateNotificationAsync(dateToNotify, notificationSettings.NotifyAt, user.Id, createdEvent, forDayBefore);
                        if (notification != null)
                            createdNotifications.Add(notification);
                    }

                    var eventDate = createdEvent.Date.Date;
                    if (notificationSettings.NotifyDayBefore)
                    {
                        TryCreateAndAddToResultList(eventDate.AddDays(-1), true);
                    }
                    TryCreateAndAddToResultList(eventDate, false);
                }
            }
            return createdNotifications;
        }

        private async Task<NotificationDto> CreateNotificationAsync(
            DateTime dateToNotify, TimeSpan timeToNotify,
            int userId, EventItemDto eventItem,
            bool forDayBefore)
        {
            if (dateToNotify > DateTime.Today)
            {
                dateToNotify = new DateTime(dateToNotify.Year, dateToNotify.Month, dateToNotify.Day,
                                            timeToNotify.Hours, timeToNotify.Minutes, timeToNotify.Seconds);

                var eventDate = eventItem.Date.Date;

                string day = forDayBefore ? "завтра" : "сегодня";

                var notification = new NotificationDto
                {
                    Subject = $"Напоминание: {day} {eventDate} {eventItem.Subject}",
                    NotificationDate = dateToNotify,
                    UserId = userId,
                    EventId = eventItem.Id
                };

                var notificationId = await CreateAsync(notification);
                return await GetByIdAsync(notificationId);
            }
            return null;
        }
    }
}
