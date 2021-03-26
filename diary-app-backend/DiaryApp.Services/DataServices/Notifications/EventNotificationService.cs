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
using DiaryApp.Services.DataInterfaces.Users;
using DiaryApp.Core.Entities;
using DiaryApp.Services.Exceptions;

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
            if (user == null)
                throw new UserNotExistsException();

            List<NotificationDto> createdNotifications = new();

            if (!IsUserAllowsNotifications(user)) return createdNotifications;

            var notificationSettings = user.Settings.NotificationSettings;
            var eventDto = await _eventItemService.GetByIdAsync(eventId);
            if (eventDto == null)
                throw new EntityNotFoundException<EventItem>();
            var createdEvent = _mapper.Map<EventItem>(eventDto);

            async Task TryCreateAndAddToResultList(DateTime dateToNotify, TimeSpan notifyAt, bool forDayBefore)
            {
                var dateWithTimeToNotify = CombineDateAndTimeToNotify(dateToNotify, notifyAt);
                var notification = await CreateNotificationAsync(createdEvent, user, dateWithTimeToNotify, forDayBefore);
                if (notification != null)
                    createdNotifications.Add(notification);
            }

            var eventDate = createdEvent.Date.Date;
            if (notificationSettings.NotifyDayBefore)
            {
                await TryCreateAndAddToResultList(eventDate.AddDays(-1), notificationSettings.NotifyAt, true);
            }
            await TryCreateAndAddToResultList(eventDate, createdEvent.Date.AddHours(-1).TimeOfDay, false);

            return createdNotifications;
        }

        private async Task<NotificationDto> CreateNotificationAsync(EventItem eventItem, UserDto user, DateTime dateToNotify, bool forDayBefore)
        {
            var today = DateTime.Today.Add(dateToNotify.TimeOfDay);
            if (dateToNotify <= today)
                return null;

            string subject = Notification.GetSubjectForEvent(eventItem, forDayBefore);

            Notification notification = new()
            {
                Subject = subject,
                NotificationDate = dateToNotify,
                UserId = user.Id,
                EventId = eventItem.Id             
            };

            await _dbSet.AddAsync(notification);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(notification.Id);
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