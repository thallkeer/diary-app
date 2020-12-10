using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;

namespace DiaryApp.Data.Services.Lists
{
    public class TodoItemService : CrudService<TodoItemDto, TodoItem>, ITodoItemService
    {
        public TodoItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
