using System;
using System.Collections.Generic;
using DiaryApp.Core;
using DiaryApp.Core.Extensions;
using DiaryApp.Core.Models;
using Xunit;
using AutoFixture;
using DiaryApp.Core.Interfaces;
using AutoFixture.Xunit2;

namespace DiaryApp.Tests
{
    public class ListCopyTests : BaseTests
    {
        [Fact]
        public void CreateDeepCopyOnTodoListShouldWorkRight()
        {
            var original = CreateList<TodoList, TodoItem>();

            var copy = original.CreateDeepCopy<TodoList, TodoItem>();

            TestLists<TodoList, TodoItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Done, copyItem.Done));
        }

        [Fact]
        public void CreateDeepCopyOnEventListShouldWorkRight()
        {
            var original = CreateList<EventList, EventItem>();

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

        [Fact]
        public void HabitTrackerGetCopyShouldCreateRightCopy()
        {
            var original = _fixture.Build<HabitTracker>()
                .Without(ht => ht.SelectedDays)
                .Without(ht => ht.GoalsArea).Create();

            var selectedDays = _fixture.Build<HabitDay>()
                .With(hd => hd.HabitTracker, original)
                .CreateMany();

            original.SelectedDays.AddRange(selectedDays);

            var copy = original.GetCopy();

            Assert.NotEqual(original, copy);
            Assert.NotEqual(original.Id, copy.Id);

            Assert.Equal(original.GoalName, copy.GoalName);
            Assert.Equal(original.SelectedDays.Count, copy.SelectedDays.Count);

            for (int i = 0; i < original.SelectedDays.Count; i++)
            {
                Assert.NotEqual(original.SelectedDays[i], copy.SelectedDays[i]);
                Assert.NotEqual(original.SelectedDays[i].Id, copy.SelectedDays[i].Id);
                Assert.NotEqual(original.SelectedDays[i].HabitTracker, copy.SelectedDays[i].HabitTracker);
                Assert.NotEqual(original.SelectedDays[i].HabitTrackerId, copy.SelectedDays[i].HabitTrackerId);

                Assert.Equal(original.SelectedDays[i].Number, copy.SelectedDays[i].Number);
                Assert.Equal(original.SelectedDays[i].Note, copy.SelectedDays[i].Note);
            }
        }

        internal static TList CreateList<TList, TItem>()
            where TList : DiaryList<TItem>, new()
            where TItem : ListItemBase, IDiaryListItem<TList, TItem>, new()
        {
            var list = _fixture.Build<TList>()
                                    .Without(l => l.Items)
                                    .Create();

            var items = _fixture.Build<TItem>()
                                    .With(i => i.OwnerID, list.Id)
                                    .With(i => i.Owner, list)
                                    .CreateMany();

            list.Items.AddRange(items);

            return list;
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
