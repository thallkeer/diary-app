using AutoMapper;
using DiaryApp.API.Models.Users;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    public class UsersController : AppBaseController<UsersController>
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService, IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserWithPasswordModel userDto)
        {
            UserDto user = userService.Authenticate(userDto.Username, userDto.Password);
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
            await userService.RegisterAsync(user, userWithPasswordModel.Password);
            return SendToken(user);
        }        

        private IActionResult SendToken(UserDto user)
        {
            string tokenString = userService.GenerateToken(user);
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