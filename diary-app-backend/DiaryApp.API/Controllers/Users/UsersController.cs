using AutoMapper;
using DiaryApp.API.Models.Users;
using DiaryApp.Models.DTO;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DiaryApp.API.Infrastructure;

namespace DiaryApp.API.Controllers
{
    public class UsersController : AppBaseController<UsersController>
    {
        private readonly IUserService _userService;
        private readonly JwtAuthManager _jwtAuthManager;

        public UsersController(IUserService userService, JwtAuthManager jwtAuthManager, IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserWithPasswordModel userDto)
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
        public async Task<IActionResult> RegisterAsync([FromBody] UserWithPasswordModel userWithPasswordModel)
        {
            var user = mapper.Map<UserDto>(userWithPasswordModel);
            await _userService.RegisterAsync(user, userWithPasswordModel.Password);
            return SendToken(user);
        }        

        private IActionResult SendToken(UserDto user)
        {
            var claims = _jwtAuthManager.GetClaims(user);
            string tokenString = _jwtAuthManager.GenerateAccessToken(claims);
            var userWithToken = new UserAuthModel
            {
                Id = user.Id,
                Username = user.Username,
                Token = tokenString
            };
            return Ok(userWithToken);
        }
    }
}