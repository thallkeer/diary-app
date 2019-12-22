using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<List<TodoItemModel>> Get()
        {
            var thingsTodo = new List<TodoItemModel>
                {
                    new TodoItemModel
                    {
                        ID = 1,
                        Text = "Написать 2 главу курсовой",
                        Done = true
                    },
                    new TodoItemModel
                    {
                        ID = 2,
                        Text = "Купить пальто",
                        Done = false
                    },
                    new TodoItemModel
                    {
                        ID = 3,
                        Text = "Забронировать отель",
                        Done = false
                    },
                    new TodoItemModel
                    {
                        ID = 4,
                        Text = "Покормить котика",
                        Done = true
                    }
                };
           

            return thingsTodo;
        }
    }
}