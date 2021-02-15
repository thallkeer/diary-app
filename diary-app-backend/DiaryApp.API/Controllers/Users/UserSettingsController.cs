using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Core.Entities.Users.Settings;

namespace DiaryApp.API.Controllers.Users
{
    public class UserSettingsController : DiaryAppContoller
    {
        private readonly ICrudService<UserSettingsDto, UserSettings> _settingsService;
        public UserSettingsController(
            ICrudService<UserSettingsDto, UserSettings> settingsService, 
            IMapper mapper) : base(mapper)
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserSettingsAsync([FromBody] UserSettingsDto userSettingsDto)
        {
            await _settingsService.UpdateAsync(userSettingsDto);
            return Ok();
        }
    }
}
