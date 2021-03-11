using AutoMapper;
using DiaryApp.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryApp.API.Controllers
{
    [ModelValidation]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class DiaryAppController : ControllerBase
    {
        private NLog.ILogger _logger;
        private IMapper _mapper;

        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected NLog.ILogger Logger => _logger ??= NLog.LogManager.GetLogger(GetType().FullName);
    }
}