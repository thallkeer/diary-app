using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class CommonListService : CrudService<CommonListDto, CommonList>, ICommonListService
    {
        public CommonListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
