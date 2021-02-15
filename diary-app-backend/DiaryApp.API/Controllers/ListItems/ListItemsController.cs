using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers.Lists
{
    public class ListItemsController : CrudController<ListItemDto, ListItem>
    {
        public ListItemsController(ICrudService<ListItemDto, ListItem> crudService, IMapper mapper) : base(crudService, mapper)
        {
        }
    }
}
