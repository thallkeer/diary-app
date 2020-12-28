using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;


namespace DiaryApp.Data.Services.Lists
{
    public class CommonListItemService : CrudService<ListItemDto, ListItem>, ICommonListItemService
    {
        public CommonListItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
