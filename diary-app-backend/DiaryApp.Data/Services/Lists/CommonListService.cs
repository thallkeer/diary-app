using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class CommonListService : CrudService<CommonListDto, CommonList>, ICommonListService
    {
        public CommonListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
