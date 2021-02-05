using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces.Lists;


namespace DiaryApp.Data.Services
{
    public class CommonListItemService : CrudService<ListItemDto, ListItem>, ICommonListItemService
    {
        public CommonListItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
