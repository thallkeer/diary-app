using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Infrastructure.ServiceInterfaces;
using DiaryApp.Services.DataInterfaces;
using System.Threading;

namespace DiaryApp.API.Controllers.Lists
{
    public class EventsController : CrudController<EventItemDto, EventItem>
    {
        private readonly IEventItemService _eventItemService;
        private readonly ISchedulerService _schedulerService;
        private readonly IUserService _userService;
        private readonly INotificationDataService _notificationService;

        public EventsController(IEventItemService eventItemService, ISchedulerService schedulerService,
            IUserService userService, INotificationDataService notificationService,
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override Task<ActionResult<int>> PostAsync([FromBody] EventItemDto createModel, CancellationToken cancellationToken = default)
        {
            var eventId = await _eventItemService.CreateAsync(createModel);

            Response.OnCompleted(async () =>
            {
                var userId = int.Parse(User.Identity.Name);
                //TODO: execute as JOB
                var createdNotifications = await _notificationService.CreateNotificationsIfNecessary(userId, eventId);
                createdNotifications.ForEach(async (notification) =>
                        await _schedulerService.ScheduleMessageAsync(notification, cancellationToken));
            });

            return Ok(eventId);
        }
    }
}
