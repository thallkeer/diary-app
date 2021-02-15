using AutoMapper;
using DiaryApp.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [ModelValidation]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class DiaryAppContoller : ControllerBase
    {
        private NLog.ILogger _logger;

        protected readonly IMapper _mapper;
        protected NLog.ILogger Logger => _logger ??= NLog.LogManager.GetLogger(GetType().FullName);

        public DiaryAppContoller(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}