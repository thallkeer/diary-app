using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces;
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