using DiaryApp.Core;
using DiaryApp.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.API
{
    public class SampleData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationContext>())
            {
                await Initialize(context);
            }
        }

        public static async Task Initialize(ApplicationContext context)
        {
            var user = new AppUser()
            {
                Username = "Keer"
            };

            var userService = new UserService(context);

            await userService.Create(user, "blablahSuperSecretPassword");

            var events = new List<EventItem>
            {
                new EventItem
                {
                    Subject = "Покормить котика",
                    Date = new DateTime(2020,1,5),
                    Description = "Необходимо покормить котика"
                },
                 new EventItem
                {
                    Subject = "Сходить в сервисный центр",
                    Date = new DateTime(2020,1,11)
                },
                  new EventItem
                {
                    Subject = "В рестик тусить",
                    Date = new DateTime(2020,1,20)
                  }
            };

            var eventList = new EventList() { Items = events, Title = "Важные события" };

            var thingsTodo = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Subject = "Написать 2 главу курсовой",
                        Done = true
                    },
                    new TodoItem
                    {
                        Subject = "Купить пальто",
                        Done = false
                    },
                    new TodoItem
                    {
                        Subject = "Забронировать отель",
                        Done = false
                    },
                    new TodoItem
                    {
                        Subject = "Покормить котика",
                        Done = true
                    }
                };

            var todoList = new TodoList() { Items = thingsTodo, Title = "Важные дела" };

            var mainPage = new MainPage
            {
                User = user,
                Month = 1,
                Year = 2020
            };

            context.MainPages.Add(mainPage);

            await context.SaveChangesAsync();

            todoList.Page = mainPage;
            eventList.Page = mainPage;

            context.TodoLists.Add(todoList);
            context.EventLists.Add(eventList);

            await context.SaveChangesAsync();

            var monthPage = new MonthPage
            {
                User = user,
                Year = 2020,
                Month = 1
            };

            context.MonthPages.Add(monthPage);

            await context.SaveChangesAsync();

            var forHome = new TodoList
            {
                Title = "Для дома",
                Items = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Subject = "Зеркало",
                        Done = true
                    },
                    new TodoItem
                    {
                        Subject = "Набор губок"
                    }
                },
                Page = monthPage
            };

            var clothesList = new TodoList
            {
                Title = "Одежда",
                Items = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Subject = "Пальто"
                    },
                    new TodoItem
                    {
                        Subject = "Водолазка",
                        Done = true
                    },
                     new TodoItem
                    {
                        Subject = "Джинсы"
                    },
                },
                Page = monthPage
            };

            await context.TodoLists.AddRangeAsync(forHome, clothesList);

            await context.SaveChangesAsync();

            var purchasesArea = new PurchasesArea
            {
                Header = "Покупки",
                PurchasesLists = new List<TodoList>
                {
                    forHome, clothesList
                },
                Page = monthPage
            };

            var desiresArea = new DesiresArea
            {
                Header = "В этом месяце я хочу",
                Page = monthPage
            };

            var ideasList = new EventList
            {
                Title = "",
                Items = new List<EventItem> {
                        new EventItem
                    {
                        Subject = "Маме на день рождения купить сертификат в Перчини",
                        Date = new DateTime(2020,1,1)
                    },
                        new EventItem
                    {
                        Subject = "На выходных выбраться за город",
                         Date = new DateTime(2020,1,1)
                    }
                    },
                Page = monthPage
            };

            await context.EventLists.AddAsync(ideasList);
            await context.SaveChangesAsync();

            var ideasArea = new IdeasArea
            {
                Header = "Идеи этого месяца",
                IdeasList = ideasList,
                Page = monthPage
            };

            var goalsArea = new GoalsArea
            {
                Header = "Цели на этот месяц",
                GoalsLists = new List<HabitsTracker>
                {
                    new HabitsTracker
                    {
                        GoalName = "Делать зарядку по утрам",
                        Month = 1,
                        Year = 2020
                    }
                },
                Page = monthPage
            };

            context.GoalsAreas.Add(goalsArea);
            context.IdeasAreas.Add(ideasArea);
            context.PurchasesAreas.Add(purchasesArea);
            context.DesiresAreas.Add(desiresArea);

            monthPage.GoalsArea = goalsArea;
            monthPage.IdeasArea = ideasArea;
            monthPage.PurchasesArea = purchasesArea;
            monthPage.DesiresArea = desiresArea;

            await context.SaveChangesAsync();
        }
    }
}
