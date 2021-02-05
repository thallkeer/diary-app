using System;
using DiaryApp.Core.Extensions;
using DiaryApp.Core.Entities;
using Xunit;
using AutoFixture;
using System.Linq;
using DiaryApp.Tests.Extensions;

namespace DiaryApp.Tests
{
    public class ListCopyTests : BaseLogicTests
    {
        [Fact]
        public void CreateDeepCopyOnTodoListShouldWorkRight()
        {
            var original = _fixture.CreateList<TodoList, TodoItem>();

            var copy = original.CreateDeepCopy<TodoList, TodoItem>();

            TestLists<TodoList, TodoItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Done, copyItem.Done));
        }

        [Fact]
        public void CreateDeepCopyOnEventListShouldWorkRight()
        {
            var original = _fixture.CreateList<EventList, EventItem>();

            var copy = original.CreateDeepCopy<EventList, EventItem>();

            TestLists<EventList, EventItem>(original, copy, (origItem, copyItem) => Assert.Equal(origItem.Date, copyItem.Date));
        }

        [Fact]
        public void CreateDeepCopyOnCommonListShouldWorkRight()
        {
            var original = _fixture.CreateList<CommonList, ListItem>();

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

            Assert.All(original.SelectedDays.Select((sd,i) => new { day = sd, index = i }), sd =>
            {
                var day = sd.day;
                var i = sd.index;
                Assert.NotEqual(day, copy.SelectedDays[i]);
                Assert.NotEqual(day.Id, copy.SelectedDays[i].Id);
                Assert.NotEqual(day.HabitTracker, copy.SelectedDays[i].HabitTracker);
                Assert.NotEqual(day.HabitTrackerId, copy.SelectedDays[i].HabitTrackerId);

                Assert.Equal(day.Number, copy.SelectedDays[i].Number);
                Assert.Equal(day.Note, copy.SelectedDays[i].Note);
            });
        }        

        internal static void TestLists<TList, TItem>(TList original, TList copy, params Action<TItem, TItem>[] itemAdditionalCheck) 
            where TList : DiaryList<TItem>
            where TItem : DiaryListItem
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
