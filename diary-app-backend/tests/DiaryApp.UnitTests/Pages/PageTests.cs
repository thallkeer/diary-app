using System;
using AutoFixture;
using AutoFixture.Xunit2;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Entities.Users;
using Xunit;

namespace DiaryApp.UnitTests.Pages
{
    public abstract class PageTests<T> : TestBase
        where T : PageBase, new()
    {
        [Theory]
        [InlineAutoData(2020, -1)]
        [InlineAutoData(2021, 0)]
        [InlineAutoData(2020, 13)]
        [InlineAutoData(1, 6)]
        [InlineAutoData(2019, 5)]
        public void Page_ShouldThrow_WhenCreatePageWithWrongMonthAndYearValues(int year, int month)
        {
            var user = _fixture.Build<AppUser>().Without(u => u.Settings).Create();
            Assert.Throws<ArgumentOutOfRangeException>(() => CreatePage(year, month, user));
        }

        [Theory]
        [InlineAutoData(2020, -1)]
        [InlineAutoData(2021, 0)]
        [InlineAutoData(2020, 13)]
        [InlineAutoData(1, 6)]
        [InlineAutoData(2019, 5)]
        public void Page_ShouldThrow_WhenWrongYearAndMonthValuesAreAssigned(int year, int month)
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

        protected abstract T CreatePage(int year, int month, AppUser user);
    }
}