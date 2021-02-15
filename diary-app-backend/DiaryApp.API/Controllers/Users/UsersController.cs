using AutoMapper;
using DiaryApp.API.Models.Users;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DiaryApp.Services.Security;
using System.Security.Claims;

namespace DiaryApp.API.Controllers
{
    public class UsersController : DiaryAppContoller
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UsersController(IUserService userService, IJwtAuthManager jwtAuthManager, IMapper mapper) : base(mapper)
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