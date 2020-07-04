using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AppBaseController<UsersController>
    {
        private readonly IUserService userService;
        private readonly AppSettings appSettings;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings,
            IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserModel userDto)
        {
            try
            {
                AppUser user = userService.Authenticate(userDto.Username, userDto.Password);
                if (user == null)
                {
                    logger.LogErrorWithDate("Username or password is incorrect");
                    return BadRequest("Username or password is incorrect");
                }
                return SendToken(user);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        private IActionResult SendToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                user.ID,
                user.Username,
                Token = tokenString
            });
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var users = userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserModel userDto)
        {
            try
            {
                var user = mapper.Map<AppUser>(userDto);
                await userService.Create(user, userDto.Password);
                return SendToken(user);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }
    }
}