using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class CommonListsController : CrudController<CommonListDto, CommonList>
    {

        public CommonListsController(ICrudService<CommonListDto, CommonList> commonListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(commonListService, mapper, loggerFactory)
        {
        }       
    }
}