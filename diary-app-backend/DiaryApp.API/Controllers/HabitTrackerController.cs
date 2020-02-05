using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitTrackerController : ControllerBase
    {
        private readonly IHabitTrackerService trackerService;
        private readonly IMapper mapper;

        public HabitTrackerController(IHabitTrackerService habitTrackerService, IMapper mapper)
        {
            this.trackerService = habitTrackerService;
            this.mapper = mapper;
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
    }
}