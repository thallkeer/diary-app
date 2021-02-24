using AutoMapper;
using DiaryApp.API.Models.Users;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Services.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using DiaryApp.Core.Entities;

namespace DiaryApp.API.Controllers
{
    public class UsersController : CrudController<UserDto, AppUser>
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UsersController(IUserService userService, IJwtAuthManager jwtAuthManager, IMapper mapper) 
            : base(userService, mapper)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserAuthRequest userDto)
        {
            UserDto user = _userService.Authenticate(userDto.Username, userDto.Password);
            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }
            return SendToken(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserAuthRequest userWithPasswordModel)
        {
            var user = _mapper.Map<UserDto>(userWithPasswordModel);
            await _userService.RegisterAsync(user, userWithPasswordModel.Password);
            return SendToken(user);
        }

        [HttpGet("{userId}/settings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserWithSettingsResponse>> GetUserSettingsAsync(int userId)
        {
            var settings = await _userService.GetSettingsAsync(userId);
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            var response = new UserWithSettingsResponse(user, settings);
            return Ok(response);
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