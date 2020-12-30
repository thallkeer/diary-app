using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces.Lists;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
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
            await UpdateAsync(_mapper.Map<TodoItemDto>(todo));
        }
    }
}
