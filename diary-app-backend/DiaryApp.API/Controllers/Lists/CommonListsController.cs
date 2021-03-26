using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO.Lists;

namespace DiaryApp.API.Controllers
{
    public class CommonListsController : CrudController<CommonListDto, CommonList>
    {

        public CommonListsController(ICrudService<CommonListDto, CommonList> commonListService)
            : base(commonListService)
        {
        }       
    }
}