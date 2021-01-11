using AutoMapper;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces.Lists;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers.Lists
{
    public class ListItemsController : CrudController<ListItemDto, ListItem>
    {
        public ListItemsController(ICommonListItemService crudService, IMapper mapper, ILoggerFactory loggerFactory) : base(crudService, mapper, loggerFactory)
        {
        }
    }
}
