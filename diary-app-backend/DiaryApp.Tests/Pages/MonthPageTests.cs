using AutoFixture;
using DiaryApp.Core.Entities;
using DiaryApp.Tests.Extensions;
using System.Collections.Generic;
using Xunit;

namespace DiaryApp.Tests
{
    public class MonthPageTests : BaseLogicTests
    {

        [Theory, MemberData(nameof(TransferDataModels))]
        public void TransferDataToNextMonth_ShouldWork(TransferDataModel transferDataModel)
        {
            var monthPage = _fixture.CreateMonthPageForNotLastMonth();
            var nextMonthPage = monthPage.TransferDataToNextMonth(transferDataModel);
            Assert.NotNull(nextMonthPage);
            Assert.Equal(monthPage.UserId, nextMonthPage.UserId);
            Assert.Equal(monthPage.User, nextMonthPage.User);
            Assert.Equal(monthPage.Year, nextMonthPage.Year);
            Assert.Equal(monthPage.Month + 1, nextMonthPage.Month);

            var pageAreas = new List<MonthPageArea>
            {
                nextMonthPage.GoalsArea,
                nextMonthPage.PurchasesArea,
                nextMonthPage.DesiresArea,
                nextMonthPage.IdeasArea
            };

            var emptyMonthPage = new MonthPage(nextMonthPage.Year, nextMonthPage.Month, nextMonthPage.User);
            emptyMonthPage.CreateAreas();

            if (!transferDataModel.GetValueForArea(nextMonthPage.GoalsArea))
            {
                Assert.Equal(emptyMonthPage.GoalsArea.Page, nextMonthPage.GoalsArea.Page);
                Assert.Equal(emptyMonthPage.GoalsArea.Header, nextMonthPage.GoalsArea.Header);
                
            }
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
    }
}