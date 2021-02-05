using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces.Lists;
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
