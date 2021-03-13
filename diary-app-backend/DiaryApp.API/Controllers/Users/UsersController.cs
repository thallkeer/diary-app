using DiaryApp.API.Models.Users;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Services.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading;
using DiaryApp.Services.DataInterfaces.Users;

namespace DiaryApp.API.Controllers
{
    public class UsersController : DiaryAppController
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UsersController(IUserService userService, IJwtAuthManager jwtAuthManager) 
            
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserAuthRequest userDto, CancellationToken cancellationToken = default)
        {
            UserDto user = await _userService.AuthenticateAsync(userDto.Username, userDto.Password);            
            return SendToken(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserAuthRequest authRequest, CancellationToken cancellationToken = default)
        {
            var user = await _userService.RegisterAsync(authRequest.Username, authRequest.Password);
            return SendToken(user);
        }

        [HttpGet("{userId}/settings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserWithSettingsResponse>> GetUserSettingsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User with such id is not exists!");
            var settings = await _userService.GetSettingsAsync(userId);    
            //settings can be null if the user has not configured them yet
            var response = new UserWithSettingsResponse(user, settings);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserDto updateRequest)
        {
            await _userService.UpdateAsync(updateRequest);
            return Ok();
        }

        private IActionResult SendToken(UserDto user)
        {
            //TODO: move to user service
            var claims = new Claim[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };
            string tokenString = _jwtAuthManager.GenerateAccessToken(claims);
            var userWithToken = new UserAuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            };
            return Ok(userWithToken);
        }
    }
}