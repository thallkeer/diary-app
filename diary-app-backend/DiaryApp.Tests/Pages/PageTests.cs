using AutoFixture.Xunit2;
using DiaryApp.Core.Entities;
using System;
using Xunit;

namespace DiaryApp.Tests
{
    public class PageTests : BaseLogicTests
    {
        [Theory]
        [InlineAutoData(2020, -1)]
        [InlineAutoData(2021, 0)]
        [InlineAutoData(2020, 13)]
        [InlineAutoData(1, 6)]
        [InlineAutoData(2019, 5)]
        public void Page_ShouldThrow_WhenCreatePageWithWrongMonthAndYearValues(int year, int month, AppUser user)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MonthPage(year, month, user));
        }

        [Theory]
        [InlineAutoData(2020, -1)]
        [InlineAutoData(2021, 0)]
        [InlineAutoData(2020, 13)]
        [InlineAutoData(1, 6)]
        [InlineAutoData(2019, 5)]
        public void Page_ShouldThrow_WhenWrongYearAndMonthValuesAreAssigned(int year, int month)
        {
            AssertThrow<MainPage>(year, month);
            AssertThrow<MonthPage>(year, month);
        }

        private void AssertThrow<T>(int year, int month) where T : PageBase, new()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var page = new T
                {
                    Year = year,
                    Month = month
                };
            });
        }

        public static TheoryData<int> WrongMonthAndYearValues()
        {
            return new TheoryData<int>
            {
                -1, 0, 13
            };
        }
    }
}
