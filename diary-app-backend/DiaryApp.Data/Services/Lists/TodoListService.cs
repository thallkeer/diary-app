using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class TodoListService : CrudService<TodoListDto, TodoList>, ITodoListService
    {
        public TodoListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
