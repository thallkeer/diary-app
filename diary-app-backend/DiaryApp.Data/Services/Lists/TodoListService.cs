using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class TodoListService : CrudService<TodoListDto, TodoList>, ITodoListService
    {
        public TodoListService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
