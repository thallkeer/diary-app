using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationContext context;

        public TodoController(ApplicationContext context) {
            this.context = context;
        }

        [HttpGet("{month:int}/title/{title}")]
        public ActionResult<TodoList> Get(int month, string title = "")
        {
            TodoList todoList = context.TodoLists.FirstOrDefault(ev => ev.Month == month && ev.Title == title);
            TodoList model = new TodoList
            {
                ID = todoList.ID,
                Month = todoList.Month,
                Title = todoList.Title,
                Items = new List<TodoItem>()
            };
            foreach (TodoItem todoModel in todoList.Items)
            {
                model.Items.Add(new TodoItem
                {
                    ID = todoModel.ID,
                    Subject = todoModel.Subject,
                    Done = todoModel.Done,
                    OwnerID = todoList.ID
                });
            }
            return model;
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody]TodoModel todoData)
        {
            var todoList = await context.TodoLists.FindAsync(todoData.OwnerID);

            var newTodo = new TodoItem
            {
                Subject = todoData.Subject,
                Done = todoData.Done,
                OwnerID = todoData.OwnerID
            };

            todoList.Items.Add(newTodo);

            int saved = await context.SaveChangesAsync();

            if (saved != 0)
            {
                todoData.ID = newTodo.ID;
                return Ok(todoData);
            }
            return BadRequest();
        }
    }
}