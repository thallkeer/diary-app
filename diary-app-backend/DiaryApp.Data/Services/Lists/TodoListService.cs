using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class TodoListService : CrudServiceWithAutoSave<TodoListDto, TodoList>, ITodoListService
    {
        public TodoListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
