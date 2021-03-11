using System.Collections.Generic;
using AutoFixture;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.UnitTests.Extensions;
using DiaryApp.UnitTests.Helpers;
using FluentAssertions;
using Xunit;

namespace DiaryApp.UnitTests.Pages
{
    public class MonthPageTests : PageTests<MonthPage>
    {
        [Fact]
        public void MonthPage_CreateAreas_ShouldCreateAreas()
        {
            var monthPage = _fixture.Create<MonthPage>();

            monthPage.CreateAreas();

            monthPage.AssertAreaParams(monthPage.PurchasesArea);
            monthPage.AssertAreaParams(monthPage.DesiresArea);
            monthPage.AssertAreaParams(monthPage.IdeasArea);
            monthPage.AssertAreaParams(monthPage.GoalsArea);

            monthPage.PurchasesArea.PurchasesLists.Should().NotBeNull();
            monthPage.DesiresArea.DesiresLists.Should().NotBeNull();
            monthPage.IdeasArea.IdeasList.Should().NotBeNull();
            monthPage.GoalsArea.GoalLists.Should().NotBeNull();
        }

        [Fact]
        public void TransferDataToNextMonth_ShouldTransferToNextYearIfPageMonthIs12()
        {

        }

        [Theory, MemberData(nameof(TransferDataModels))]
        public void TransferDataToNextMonth_ShouldCreateNextMonthPage(TransferDataModel transferDataModel)
        {
            var monthPage = _fixture.CreateMonthPageWithNon12Month();
            var nextMonthPage = monthPage.TransferDataToNextMonth(transferDataModel);

            nextMonthPage.Should().NotBeNull();
            nextMonthPage.User.Should().Be(monthPage.User);
            nextMonthPage.Year.Should().Be(monthPage.Year);
            nextMonthPage.Month.Should().Be(monthPage.Month + 1);
        }

        [Theory, MemberData(nameof(TransferDataModels))]
        public void TransferDataToNextMonth_ShouldCreateInitializedAreaIfItWasNotTransferred(TransferDataModel transferDataModel)
        {
            var monthPage = _fixture.CreateMonthPageWithNon12Month();
            var nextMonthPage = monthPage.TransferDataToNextMonth(transferDataModel);

            new List<MonthPageArea>
            {
                nextMonthPage.GoalsArea,
                nextMonthPage.PurchasesArea,
                nextMonthPage.DesiresArea,
                nextMonthPage.IdeasArea
            }.ForEach(pa =>
            {
                if (!transferDataModel.GetValueForArea(pa))
                {
                    pa.Should().NotBeNull();
                }
            });
        }

        public static TheoryData<TransferDataModel> TransferDataModels()
        {
            var fixture = new Fixture();
            return new TheoryData<TransferDataModel>
            {
                fixture.Create<TransferDataModel>(),
                TransferDataModel.CreateFullTransferModel(),
                new TransferDataModel()
            };
        }

        protected override MonthPage CreatePage(int year, int month, AppUser user) => new(year, month, user);
    }
}