using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services.Lists
{
    public class TodoItemService : CrudServiceWithAutoSave<TodoItemDto, TodoItem>, ITodoItemService
    {
        public TodoItemService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task ToggleAsync(int todoId)
        {
            var todo = await GetByIdAsync(todoId);
            todo.Done = !todo.Done;
            await UpdateAsync(todo);
        }
    }
}
