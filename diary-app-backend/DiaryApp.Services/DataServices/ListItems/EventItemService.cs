using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using System.Threading.Tasks;
using DiaryApp.Services.ServiceInterfaces;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Models.DTO.Notifications;
using DiaryApp.Core.Entities.Notifications;
using System;

namespace DiaryApp.Services.Services
{
    public class EventItemService : CrudService<EventItemDto, EventItem>, IEventItemService
    {
        private readonly ISchedulerService _schedulerService;
        private readonly ICrudService<NotificationDto, Notification> _notificationService;
        private readonly IUserService _userService;

        public EventItemService(ApplicationContext context, IMapper mapper, 
            ISchedulerService schedulerService, ICrudService<NotificationDto, Notification> notificationService, IUserService userService) 
            : base(context, mapper)
        {
            _schedulerService = schedulerService;
            _notificationService = notificationService;
            _userService = userService;
        }

        public async Task<int> CreateWithNotificationAsync(EventItemDto dto, int userId)
        {
            var eventId = await CreateAsync(dto);
            var createdEvent = await GetByIdAsync(eventId);
            var user = await _userService.FirstOrDefaultAsync(u => u.Id == userId);

            if (user.TelegramId.HasValue) 
            {
                var notificationSettings = user.Settings?.NotificationSettings;
                if (notificationSettings != null && notificationSettings.IsActivated)
                {
                    var dateToNotify = createdEvent.Date.Date.AddDays(-1);
                    if (dateToNotify != DateTime.Today)
                    {
                        dateToNotify = new DateTime(dateToNotify.Year, dateToNotify.Month, dateToNotify.Day, 10, 0, 0);

                        var notification = new NotificationDto
                        {
                            Subject = createdEvent.Subject,
                            NotificationDate = dateToNotify,
                            UserId = user.Id
                        };
                        var notificationId = await _notificationService.CreateAsync(notification);
                        var createdNotification = await _notificationService.GetByIdAsync(notificationId);

                        await _schedulerService.ScheduleMessageAsync(createdNotification);
                    }
                }
            }

            return eventId;
        }

        public async override Task<int> CreateAsync(EventItemDto dto)
        {
            dto.Date = dto.Date.ToLocalTime();
            var eventId = await base.CreateAsync(dto);
            return eventId;
        }
    }
}
