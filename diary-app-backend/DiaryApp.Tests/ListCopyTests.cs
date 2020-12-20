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
            var original = CreateList<TodoList, TodoItem>((todo) =>
            {
                todo.Done = true;
            });

            var copy = original.CreateDeepCopy<TodoList, TodoItem>();

            TestLists<TodoList, TodoItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Done, copyItem.Done));
        }

        [Fact]
        public void CreateDeepCopyOnEventListShouldWorkRight()
        {
            var original = CreateList<EventList, EventItem>((ev) => {
                ev.Date = DateTime.Now.AddDays(1);
            });

            var copy = original.CreateDeepCopy<EventList, EventItem>();

            TestLists<EventList, EventItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Date, copyItem.Date));
        }

        [Fact]
        public void CreateDeepCopyOnCommonListShouldWorkRight()
        {
            var original = CreateList<CommonList, ListItem>();

            var copy = original.CreateDeepCopy<CommonList, ListItem>();

            TestLists<CommonList, ListItem>(original, copy);
        }

        internal static TList CreateList<TList, TItem>(Action<TItem> customItemSeed = null)
            where TList : DiaryList<TItem>, new()
            where TItem : ListItemBase, new()
        {
            var original = new TList
            {
                Id = 1,
                Title = $"Test{typeof(TList)}",
                Items = new List<TItem>()
            };

            for (int i = 0; i < 5; i++)
            {
                var item = new TItem
                {
                    Id = i + 1,
                    Subject = $"test subject {i}",
                    OwnerID = original.Id,
                    Url = $"www.test-{i}.com"
                };
                customItemSeed?.Invoke(item);
                original.Items.Add(item);
            }

            return original;
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
