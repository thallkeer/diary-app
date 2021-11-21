using DiaryApp.Core.Entities;
using DiaryApp.UnitTests.Extensions;
using FluentAssertions;
using FluentAssertions.Common;
using System.Collections.Generic;
using System.Linq;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Core.Entities.PageAreas;
using Xunit;

namespace DiaryApp.UnitTests
{
    public class PageAreasTests : TestBase
    {
        [Theory]
        [MemberData(nameof(TheoryPurchasesAreas))]
        public void PurchasesArea_AddFromOtherArea_ShouldWork(PurchasesArea originalArea)
        {
            var otherArea = new PurchasesArea(null, true);

            var areaTotalListsCountBeforeAddFromOtherArea = originalArea.PurchasesLists.Count;
            var emptyListsCountBeforeAddFromOtherArea = originalArea.PurchasesLists.Count(pl => pl.Items.Count == 0);

            var otherAreaTotalListsCount = otherArea.PurchasesLists.Count;
            var otherAreaEmptyListsCount = otherArea.PurchasesLists.Count(pl => pl.Items.Count == 0);

            originalArea.AddDataFromOtherArea(otherArea);

            var emptyListsCountAfterAddFromOtherArea = originalArea.PurchasesLists.Count(pl => pl.Items.Count == 0);
            otherAreaEmptyListsCount.Should().Be(emptyListsCountAfterAddFromOtherArea);

            var areaTotalListsCountAfterAddFromOtherArea = otherAreaTotalListsCount + areaTotalListsCountBeforeAddFromOtherArea - emptyListsCountBeforeAddFromOtherArea;
            originalArea.PurchasesLists.Count.Should().Be(areaTotalListsCountAfterAddFromOtherArea);
        }

        [Theory]
        [MemberData(nameof(TheoryDesiresAreas))]
        public void DesiresAreaAddFromOtherAreaShouldWork(DesiresArea originalArea)
        {
            var otherArea = new DesiresArea(null, true);
            originalArea.AddDataFromOtherArea(otherArea);

            var itemsCountByListInputArea = originalArea.DesiresLists.ToDictionary(k => k.List.Title, v => v.Items.Count);
            var itemsCountByListOtherArea = otherArea.DesiresLists.ToDictionary(k => k.List.Title, v => v.Items.Count);

            originalArea.DesiresLists.Count.Should().Be(3);
            originalArea.DesiresLists[0].List.Title.Should().Be(DesiresArea.ToVisitStr);
            originalArea.DesiresLists[1].List.Title.Should().Be(DesiresArea.ToWatchStr);
            originalArea.DesiresLists[2].List.Title.Should().Be(DesiresArea.ToReadStr);

            originalArea.DesiresLists.ForEach(dl =>
            {
                int totalItemsCount = itemsCountByListInputArea[dl.List.Title] + itemsCountByListOtherArea[dl.List.Title];
                dl.Items.Count.Should().Be(totalItemsCount);
            });
        }

        [Theory]
        [MemberData(nameof(TheoryIdeasAreas))]
        public void IdeasAreaAddFromOtherAreaShouldWork(IdeasArea originalArea)
        {
            var otherArea = new IdeasArea(null, true);

            int itemsCountInputArea = originalArea.IdeasList?.Items.Count ?? 0;
            int itemsCountOtherArea = otherArea.IdeasList.Items.Count;

            originalArea.AddDataFromOtherArea(otherArea);

            originalArea.IdeasList.Should().NotBeNull();
            originalArea.IdeasList.Items.Count.Should().Be(itemsCountInputArea + itemsCountOtherArea);
        }

        [Theory]
        [MemberData(nameof(TheoryGoalsAreas))]
        public void GoalsAreaAddFromOtherAreaShouldWork(GoalsArea originalArea)
        {
            var otherArea = new GoalsArea(null, true);

            var listsCountInputArea = originalArea.GoalLists.Count;
            var emptyListsCountInputArea = originalArea.GoalLists.Where(gl => gl.SelectedDays.Count == 0 && gl.GoalName == GoalsArea.GoalNameStr).Count();

            var listsCountOtherArea = otherArea.GoalLists.Count;
            var nonEmptyListsCountOtherArea = otherArea.GoalLists.Where(gl => gl.GoalName != GoalsArea.GoalNameStr).Count();

            var selectedDaysInputAreaCount = originalArea.GoalLists.Count(gl => gl.SelectedDays.Count != 0);
            var selectedDaysOtherAreaCount = otherArea.GoalLists.Count(gl => gl.SelectedDays.Count != 0);

            originalArea.AddDataFromOtherArea(otherArea);

            originalArea.GoalLists.Count(gl => gl.SelectedDays.Count != 0).IsSameOrEqualTo(selectedDaysOtherAreaCount + selectedDaysInputAreaCount);

            if (listsCountInputArea == emptyListsCountInputArea && nonEmptyListsCountOtherArea == 0)
            {
                //if after add from other area goals lists will not contains any items, area will initialize itself with one default list
                originalArea.GoalLists.Count.Should().Be(1);
            }
            else
            {
                var expectedTotalListsCountAfterAdd = listsCountInputArea - emptyListsCountInputArea + nonEmptyListsCountOtherArea;
                originalArea.GoalLists.Count.Should().Be(expectedTotalListsCountAfterAdd);
            }               
        }       

        #region TestData
        public static TheoryData<PurchasesArea> TheoryPurchasesAreas()
        {
            var paWithCustomLists = new PurchasesArea(null, false);

            for (int i = 0; i < 2; i++)
            {
                var pa = _fixture.CreateListWrapper<PurchaseList, TodoList, TodoItem, PurchasesArea>(i % 2 == 0);
                paWithCustomLists.PurchasesLists.Add(pa);
            }
            return new TheoryData<PurchasesArea>
            {
                new PurchasesArea(null, true),
                new PurchasesArea(null, false),
                paWithCustomLists
            };
        }

        public static TheoryData<DesiresArea> TheoryDesiresAreas()
        {
            var paWithInitializeAndItems = new DesiresArea(null, true);
            paWithInitializeAndItems.DesiresLists.ForEach(dl =>
            {
                if (dl.List.Title != DesiresArea.ToVisitStr)
                    dl.List.Items.Add(new ListItem());
            });

            return new TheoryData<DesiresArea>
            {
                new DesiresArea(null, true),
                new DesiresArea(null, false),
                paWithInitializeAndItems                
            };
        }

        public static TheoryData<IdeasArea> TheoryIdeasAreas()
        {
            var paWithInitializeAndItems = new IdeasArea(null, true);
            paWithInitializeAndItems.IdeasList.Add(new ListItem());

            return new TheoryData<IdeasArea>
            {
                new IdeasArea(null, true),
                new IdeasArea(null, false),
                paWithInitializeAndItems
            };
        }

        public static TheoryData<GoalsArea> TheoryGoalsAreas()
        {
            var paWithCustomLists = new GoalsArea(null, false);
            var pa = new HabitTracker
            {
                GoalName = GoalsArea.GoalNameStr,
                SelectedDays = new List<HabitDay>()
            };
            pa.SelectedDays.Add(new HabitDay());

            var trackerWithCustomNameAndSelectedDays = new HabitTracker
            {
                GoalName = "custom name with selected days",
                SelectedDays = new List<HabitDay>()
            };
            trackerWithCustomNameAndSelectedDays.SelectedDays.Add(new HabitDay());

            var trackerWithCustomNameAndWithoutSelectedDays = new HabitTracker
            {
                GoalName = "custom name without selected days",
                SelectedDays = new List<HabitDay>()
            };

            paWithCustomLists.GoalLists.AddRange(new HabitTracker[] {
                pa, trackerWithCustomNameAndSelectedDays, trackerWithCustomNameAndWithoutSelectedDays
            });

            return new TheoryData<GoalsArea>
            {
                new GoalsArea(null, true),
                new GoalsArea(null, false),
                paWithCustomLists
            };
        }

        #endregion
    }
}

