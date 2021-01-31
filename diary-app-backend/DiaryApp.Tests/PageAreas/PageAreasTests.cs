using DiaryApp.Core.Models;
using DiaryApp.Tests.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DiaryApp.Tests
{
    public class PageAreasTests : BaseLogicTests
    {
        [Theory]
        [MemberData(nameof(TheoryPurchasesAreas))]
        public void PurcshasesAreaAddFromOtherAreaShouldWorkFine(PurchasesArea purchasesArea)
        {
            var otherArea = new PurchasesArea(null, true);

            var areaTotalListsCountBeforeAddFromOtherArea = purchasesArea.PurchasesLists.Count;
            var emptyListsCountBeforeAddFromOtherArea = purchasesArea.PurchasesLists.Count(pl => pl.Items.Count == 0);
            var otherAreaEmptyListsCount = otherArea.PurchasesLists.Count(pl => pl.Items.Count == 0);
            var otherAreaTotalListsCount = otherArea.PurchasesLists.Count;

            purchasesArea.AddFromOtherArea(otherArea);
            var emptyListsCountAfterAddFromOtherArea = purchasesArea.PurchasesLists.Count(pl => pl.Items.Count == 0);

            Assert.Equal(purchasesArea.PurchasesLists.Count, otherAreaTotalListsCount + areaTotalListsCountBeforeAddFromOtherArea - emptyListsCountBeforeAddFromOtherArea);
            Assert.Equal(emptyListsCountAfterAddFromOtherArea, otherAreaEmptyListsCount);
        }

        [Theory]
        [MemberData(nameof(TheoryDesiresAreas))]
        public void DesiresAreaAddFromOtherAreaShouldWorkFine(DesiresArea desiresArea)
        {
            var other = new DesiresArea(null, true);
            desiresArea.AddFromOtherArea(other);

            var itemsCountByListInputArea = desiresArea.DesiresLists.ToDictionary(k => k.List.Title, v => v.Items.Count);
            var itemsCountByListOtherArea = other.DesiresLists.ToDictionary(k => k.List.Title, v => v.Items.Count);

            Assert.True(desiresArea.DesiresLists.Count == 3);
            Assert.True(desiresArea.DesiresLists[0].List.Title == DesiresArea.ToVisitSTR);
            Assert.True(desiresArea.DesiresLists[1].List.Title == DesiresArea.ToWatchSTR);
            Assert.True(desiresArea.DesiresLists[2].List.Title == DesiresArea.ToReadSTR);

            desiresArea.DesiresLists.ForEach(dl =>
            {
                int totalItemsCount = itemsCountByListInputArea[dl.List.Title] + itemsCountByListOtherArea[dl.List.Title];
                Assert.True(dl.Items.Count == totalItemsCount);
            });
        }

        [Theory]
        [MemberData(nameof(TheoryIdeasAreas))]
        public void IdeasAreaAddFromOtherAreaShouldWorkFine(IdeasArea ideasArea)
        {
            var other = new IdeasArea(null, true);

            int itemsCountInputArea = ideasArea.IdeasList?.Items.Count ?? 0;
            int itemsCountOtherArea = other.IdeasList.Items.Count;

            ideasArea.AddFromOtherArea(other);

            Assert.Equal(ideasArea.IdeasList.Items.Count, itemsCountInputArea + itemsCountOtherArea);
        }

        [Theory]
        [MemberData(nameof(TheoryGoalsAreas))]
        public void GoalsAreaAddFromOtherAreaShouldWorkFine(GoalsArea goalsArea)
        {
            var other = new GoalsArea(null, true);

            var listsCountInputArea = goalsArea.GoalLists.Count;
            var emptyListsCountInputArea = goalsArea.GoalLists.Where(gl => gl.SelectedDays.Count == 0 && gl.GoalName == GoalsArea.GoalNameSTR).Count();

            var listsCountOtherArea = other.GoalLists.Count;
            var nonEmptyListsCountOtherArea = other.GoalLists.Where(gl => gl.GoalName != GoalsArea.GoalNameSTR).Count();

            var selectedDaysInputAreaCount = goalsArea.GoalLists.Count(gl => gl.SelectedDays.Count != 0);
            var selectedDaysOtherAreaCount = other.GoalLists.Count(gl => gl.SelectedDays.Count != 0);

            goalsArea.AddFromOtherArea(other);

            Assert.Equal(goalsArea.GoalLists.Count(gl => gl.SelectedDays.Count != 0), selectedDaysOtherAreaCount + selectedDaysInputAreaCount);

            if (listsCountInputArea == emptyListsCountInputArea && nonEmptyListsCountOtherArea == 0)
            {
                //if after add from other area goals lists will not contains any items, area will initialize itself with one default list
                Assert.True(goalsArea.GoalLists.Count == 1);
            }
            else
                Assert.True(goalsArea.GoalLists.Count == listsCountInputArea - emptyListsCountInputArea + nonEmptyListsCountOtherArea);
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
            var paWithInitialize = new DesiresArea(null, true);
            var paWithInitializeAndItems = new DesiresArea(null, true);
            paWithInitializeAndItems.DesiresLists.ForEach(dl =>
            {
                if (dl.List.Title != DesiresArea.ToVisitSTR)
                    dl.List.Items.Add(new ListItem());
            });

            return new TheoryData<DesiresArea>
            {
                paWithInitialize,
                paWithInitializeAndItems,
                new DesiresArea(null, false)
            };
        }

        public static TheoryData<IdeasArea> TheoryIdeasAreas()
        {
            var paWithInitialize = new IdeasArea(null, true);
            var paWithInitializeAndItems = new IdeasArea(null, true);
            paWithInitializeAndItems.IdeasList.Items.Add(new ListItem());

            return new TheoryData<IdeasArea>
            {
                paWithInitialize,
                paWithInitializeAndItems,
                new IdeasArea(null, false)
            };
        }

        public static TheoryData<GoalsArea> TheoryGoalsAreas()
        {
            var paWithCustomLists = new GoalsArea(null, false);
            var pa = new HabitTracker
            {
                GoalName = GoalsArea.GoalNameSTR,
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

