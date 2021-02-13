﻿using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using System.Threading.Tasks;

namespace DiaryApp.Data.DataInterfaces.Lists
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