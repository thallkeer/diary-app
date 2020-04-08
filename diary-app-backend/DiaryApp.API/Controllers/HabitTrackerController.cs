using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitTrackerController : AppBaseController<HabitTrackerController>
    {
        private readonly IHabitTrackerService trackerService;

        public HabitTrackerController(IHabitTrackerService habitTrackerService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper,loggerFactory)
        {
            this.trackerService = habitTrackerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTracker([FromBody] HabitsTrackerModel trackerModel)
        {
            var tracker = mapper.Map<HabitsTracker>(trackerModel);
            await trackerService.Create(tracker);
            return Ok(tracker.ID);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTracker([FromBody] HabitsTrackerModel trackerModel)
        {
            var tracker = mapper.Map<HabitsTracker>(trackerModel);
            await trackerService.Update(tracker);
            return Ok();
        }

        [HttpDelete("{trackerID}")]
        public async Task<IActionResult> DeleteTracker(int trackerID)
        {
            await trackerService.Delete(trackerID);
            return Ok();
        }
    }
}