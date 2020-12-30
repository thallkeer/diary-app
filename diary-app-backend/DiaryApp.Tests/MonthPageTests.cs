using AutoFixture;
using AutoFixture.Xunit2;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Tests.Customizations;
using DiaryApp.Tests.Extensions;
using DiaryApp.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiaryApp.Tests
{
    public class MonthPageTests
    {
        private IFixture _fixture;

        public MonthPageTests()
        {
            _fixture = Configurations.GetFixture();
        }

        [Theory, MemberData(nameof(WrongMonthAndYearValues))]
        public void TransferDataToNextMonth_ShouldWork(TransferDataModel transferDataModel)
        {
            var monthPage = _fixture.Create<MonthPage>();
            var nextMonthPage = monthPage.TransferDataToNextMonth(transferDataModel);
            Assert.NotNull(nextMonthPage);
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
                Assert.Equal(emptyMonthPage.GoalsArea.GetHashCode(), nextMonthPage.GoalsArea.GetHashCode());
                Assert.Equal(emptyMonthPage.GoalsArea, nextMonthPage.GoalsArea);
            }
        }

        public static TheoryData<TransferDataModel> WrongMonthAndYearValues()
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