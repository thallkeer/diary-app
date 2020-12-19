using System;
using System.Collections.Generic;
using DiaryApp.Core.Extensions;
using DiaryApp.Core.Models;
using Xunit;

namespace DiaryApp.Tests
{
    public class ListCopyTests
    {
        [Fact]
        public void CreateDeepCopyOnTodoListShouldWorkRight()
        {
            var original = new TodoList
            {
                Id = 1,
                Title = "TodoTestList",
                Items = new List<TodoItem>()
            };

            for (int i = 0; i < 5; i++)
            {
                var todoItem = new TodoItem
                {
                    Id = i + 1,
                    Done = i % 2 == 0,
                    Subject = $"test subject {i}",
                    Owner = original,
                    OwnerID = original.Id,
                    Url = $"www.test-{i}.com"
                };
                original.Items.Add(todoItem);
            }

            var copy = original.CreateDeepCopy<TodoList, TodoItem>();

            TestLists<TodoList, TodoItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Done, copyItem.Done));           
        }

        [Fact]
        public void CreateDeepCopyOnEventListShouldWorkRight()
        {
            var original = new EventList
            {
                Id = 1,
                Title = "EventTestList",
                Items = new List<EventItem>()
            };

            for (int i = 0; i < 5; i++)
            {
                var eventItem = new EventItem
                {
                    Id = i + 1,
                    Subject = $"test subject {i}",
                    Owner = original,
                    OwnerID = original.Id,
                    Url = $"www.test-{i}.com",
                    Date = DateTime.Now.AddDays(i)
                };
                original.Items.Add(eventItem);
            }

            var copy = original.CreateDeepCopy<EventList, EventItem>();

            TestLists<EventList, EventItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Date, copyItem.Date));
        }

        internal static void TestLists<TList, TItem>(TList original, TList copy, params Action<TItem, TItem>[] itemAdditionalCheck) 
            where TList : DiaryList<TItem>
            where TItem : ListItemBase
        {
            Assert.NotEqual(original, copy);
            Assert.NotEqual(original.Id, copy.Id);

            Assert.Equal(original.Title, copy.Title);
            Assert.Equal(original.Items.Count, copy.Items.Count);            

            for (int i = 0; i < original.Items.Count; i++)
            {
                Assert.NotEqual(original.Items[i], copy.Items[i]);
                Assert.NotEqual(original.Items[i].Id, copy.Items[i].Id);
                Assert.Null(copy.Items[i].Owner);
                Assert.NotEqual(original.Items[i].OwnerID, copy.Items[i].OwnerID);

                Assert.Equal(original.Items[i].Subject, copy.Items[i].Subject);
                Assert.Equal(original.Items[i].Url, copy.Items[i].Url);

                foreach (var itemCheck in itemAdditionalCheck)
                {
                    itemCheck(original.Items[i], copy.Items[i]);
                }
            }
        }
    }
}
