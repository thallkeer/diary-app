using AutoMapper;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces;
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
        public async Task<IActionResult> AddTracker([FromBody] HabitTrackerDto trackerModel)
        {
            var trackerId = await trackerService.CreateAsync(trackerModel);
            return Ok(trackerId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTracker([FromBody] HabitTrackerDto trackerModel)
        {
            await trackerService.UpdateAsync(trackerModel);
            return Ok();
        }

        [HttpDelete("{trackerID}")]
        public async Task<IActionResult> DeleteTracker(int trackerID)
        {
            await trackerService.DeleteAsync(trackerID);
            return Ok();
        }
    }
}