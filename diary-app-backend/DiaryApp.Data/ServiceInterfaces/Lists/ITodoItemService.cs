using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces.Lists
{
    public interface ITodoItemService : ICrudService<TodoItemDto, TodoItem>
    {
        /// <summary>
        /// Toggles todo's state
        /// </summary>
        /// <param name="todoId">Todo item id</param>
        Task ToggleAsync(int todoId);
    }
}
