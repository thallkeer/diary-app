using AutoFixture;
using DiaryApp.Core.Entities;
using System.Linq;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.UnitTests.Extensions;

namespace DiaryApp.UnitTests.Customizations
{
    public class DiaryListWrappersCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.CustomizeListWrapper<PurchaseList, TodoList, TodoItem, PurchasesArea>();
            fixture.CustomizeListWrapper<DesiresList, CommonList, ListItem, DesiresArea>();
            fixture.CustomizeListWrapper<IdeasList, CommonList, ListItem, IdeasArea>();
        }
    }

    public class DiaryListsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.CustomizeList<CommonList, ListItem>();
            fixture.CustomizeList<EventList, EventItem>();
            fixture.CustomizeList<TodoList, TodoItem>();
            fixture.Customize<HabitTracker>(composer =>
                    composer.With(ht => ht.SelectedDays,
                                        fixture.Build<HabitDay>()
                                               .Without(hd => hd.HabitTracker)
                                               .CreateMany()
                                               .ToList())
                             .Without(ht => ht.GoalsArea));
        }
    }
}
