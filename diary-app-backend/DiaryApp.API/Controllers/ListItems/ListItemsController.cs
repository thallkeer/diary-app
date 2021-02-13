using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using Microsoft.Extensions.Logging;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers.Lists
{
    public class ListItemsController : CrudController<ListItemDto, ListItem>
    {
        public ListItemsController(ICrudService<ListItemDto, ListItem> crudService, IMapper mapper, ILoggerFactory loggerFactory) : base(crudService, mapper, loggerFactory)
        {
        }
    }
}
