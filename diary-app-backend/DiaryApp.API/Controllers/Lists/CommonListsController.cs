using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonListsController : AppBaseController<CommonListsController>
    {

        public CommonListsController(IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
        }       
    }
}