using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.Notifications;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Lists;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Services.Services;
using System;
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

        public async Task<NotificationDto> TryCreateNotificationAsync(int userId, int eventId)
        {
            var createdEvent = await _eventItemService.GetByIdAsync(eventId);
            var user = await _userService.FirstOrDefaultAsync<UserWithSettingsDto>(u => u.Id == userId);

            if (user.TelegramId.HasValue)
            {
                var notificationSettings = user.Settings?.NotificationSettings;
                if (notificationSettings != null && notificationSettings.IsActivated)
                {
                    var eventDate = createdEvent.Date.Date;
                    var dateToNotify = eventDate.AddDays(-1);
                    if (dateToNotify != DateTime.Today)
                    {
                        dateToNotify = new DateTime(dateToNotify.Year, dateToNotify.Month, dateToNotify.Day, 10, 0, 0);

                        var notification = new NotificationDto
                        {
                            Subject = $"Напоминание: завтра \"{eventDate}\" {createdEvent.Subject}",
                            NotificationDate = dateToNotify,
                            UserId = user.Id,
                            EventId = createdEvent.Id
                        };

                        var notificationId = await CreateAsync(notification);
                        return await GetByIdAsync(notificationId);
                    }
                }
            }

            return null;
        }
    }
}
