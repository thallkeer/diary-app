using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;


namespace DiaryApp.Data.Services
{
    public class CommonListItemService : CrudService<ListItemDto, ListItem>, ICommonListItemService
    {
        public CommonListItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
