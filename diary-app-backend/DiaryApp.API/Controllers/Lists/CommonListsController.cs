using AutoMapper;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class CommonListsController : CrudController<CommonListDto, CommonList>
    {

        public CommonListsController(ICommonListService commonListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(commonListService, mapper, loggerFactory)
        {
        }       
    }
}