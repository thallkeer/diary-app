using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Lists;
using System.Threading.Tasks;

namespace DiaryApp.Services.Services
{
    public class TodoItemService : CrudService<TodoItemDto, TodoItem>, ITodoItemService
    {
        public TodoItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task ToggleAsync(int todoId)
        {
            var todo = await GetByIdAsync(todoId);
            todo.Done = !todo.Done;
            await _context.SaveChangesAsync();
        }
    }
}
