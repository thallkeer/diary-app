using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Infrastructure.ServiceInterfaces;
using DiaryApp.Services.DataInterfaces;
using System;
using DiaryApp.Services.DTO.Notifications;
using DiaryApp.Core.Entities.Notifications;

namespace DiaryApp.API.Controllers.Lists
{
    public class EventsController : CrudController<EventItemDto, EventItem>
    {
        private readonly IEventItemService _eventItemService;
        private readonly ISchedulerService _schedulerService;
        private readonly IUserService _userService;
        private readonly ICrudService<NotificationDto, Notification> _notificationService;

        public EventsController(IEventItemService eventItemService, ISchedulerService schedulerService, 
            IUserService userService, ICrudService<NotificationDto, Notification> notificationService,
            IMapper mapper) 
            : base(eventItemService, mapper)
        {
            _eventItemService = eventItemService;
            _schedulerService = schedulerService;
            _userService = userService;
            _notificationService = notificationService;
        }


        /// <summary>
        /// Creates new event at "Important Events" list. And schedules a notification 
        /// about this event if it's possible.
        /// </summary>
        /// <param name="createModel">Event model</param>
        /// <returns></returns>
        public async override Task<ActionResult<int>> PostAsync([FromBody] EventItemDto createModel)
        {
            var userIdString = User.Identity.Name;
            if (string.IsNullOrEmpty(userIdString))
                return await base.PostAsync(createModel);
            var userId = int.Parse(userIdString);
            var eventId = await _eventItemService.CreateAsync(createModel);
            var createdEvent = await _eventItemService.GetByIdAsync(eventId);
            var user = await _userService.FirstOrDefaultAsync(u => u.Id == userId);

            ///TODO: move to service
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
            return Ok(eventId);
        }
    }
}
