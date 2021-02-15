using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers
{
    public class CommonListsController : CrudController<CommonListDto, CommonList>
    {

        public CommonListsController(ICrudService<CommonListDto, CommonList> commonListService, IMapper mapper)
            : base(commonListService, mapper)
        {
        }       
    }
}