using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.ListItems;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Infrastructure.ServiceInterfaces;
using DiaryApp.Services.DataInterfaces;
using System.Threading;

namespace DiaryApp.API.Controllers.ListItems
{
    public class EventsController : CrudController<EventItemDto, EventItem>
    {
        private readonly IEventItemService _eventItemService;
        private readonly ISchedulerService _schedulerService;
        private readonly IEventNotificationService _notificationService;

        public EventsController(IEventItemService eventItemService, ISchedulerService schedulerService, IEventNotificationService notificationService)
            : base(eventItemService)
        {
            _eventItemService = eventItemService;
            _schedulerService = schedulerService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Creates new event at "Important Events" list. And schedules a notification 
        /// about this event if it's possible.
        /// </summary>
        /// <param name="createModel">Event model</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<ActionResult<int>> PostAsync([FromBody] EventItemDto createModel, CancellationToken cancellationToken = default)
        {
            var eventId = await _eventItemService.CreateAsync(createModel);

            Response.OnCompleted(async () =>
            {
                var userId = UserId;
                //TODO: execute as JOB
                var createdNotifications = await _notificationService.CreateNotificationsIfNecessary(userId, eventId);
                createdNotifications.ForEach(async (notification) =>
                        await _schedulerService.ScheduleMessageAsync(notification, cancellationToken));
            });

            return Ok(eventId);
        }
    }
}
