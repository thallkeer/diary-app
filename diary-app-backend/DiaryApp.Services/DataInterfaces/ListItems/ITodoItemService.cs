using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces.ListItems
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
