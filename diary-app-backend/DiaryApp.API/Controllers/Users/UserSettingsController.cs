using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Core.Entities.Users.Settings;

namespace DiaryApp.API.Controllers.Users
{
    public class UserSettingsController : DiaryAppController
    {
        private readonly ICrudService<UserSettingsDto, UserSettings> _settingsService;

        public UserSettingsController(ICrudService<UserSettingsDto, UserSettings> settingsService) 
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserSettingsAsync([FromBody] UserSettingsDto updateRequest)
        {
            await _settingsService.UpdateAsync(updateRequest);
            return Ok();
        }
    }
}