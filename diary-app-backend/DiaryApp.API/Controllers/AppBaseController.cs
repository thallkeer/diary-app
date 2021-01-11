using AutoMapper;
using DiaryApp.API.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [ModelValidation]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class AppBaseController<T> : ControllerBase where T : ControllerBase
    {
        protected readonly IMapper mapper;
        protected readonly ILogger logger;

        public AppBaseController(IMapper mapper, ILoggerFactory loggerFactory)
        {
            this.mapper = mapper;
            //loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), $"{typeof(T).Name}.txt"));
            this.logger = loggerFactory.CreateLogger<T>();
        }
    }
}