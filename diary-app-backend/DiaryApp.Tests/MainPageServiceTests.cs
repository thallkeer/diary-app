using DiaryApp.Core.DTO;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DiaryApp.Tests
{
    public class MainPageServiceTests : BaseTests
    {
        const int userId = 1;
        const int year = 2020;
        const int month = 10;

        [Fact]
        public async Task ShouldReturnPageIfExists()
        {
            // arrange
            int pageId = 2;

            MainPageDto mainPage = await new MainPageService(_dbContext, _mapper)
                                        .GetPageForUser(userId, year, month);

            // assert
            Assert.True(mainPage != null);
            Assert.Equal(pageId, mainPage.Id);
            Assert.Equal(userId, mainPage.UserID);
            Assert.Equal(year, mainPage.Year);
            Assert.Equal(month, mainPage.Month);     
        }

        [Fact]
        public async Task CreatePageByParamsShouldFailIfPageExists()
        {
            var service = new MainPageService(_dbContext, _mapper);

            await Assert.ThrowsAsync<PageAlreadyExistsException>(async () => await service.CreatePageByParams(userId, year, month));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfMonthValueIsWrong(int inputMonth)
        {
            var service = new MainPageService(_dbContext, _mapper);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreatePageByParams(userId, year, inputMonth));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfYearValueIsWrong(int inputYear)
        {
            var service = new MainPageService(_dbContext, _mapper);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreatePageByParams(userId, inputYear, month));
        }        

        [Fact]
        public async Task CreatePageByParamsShouldCreatePage()
        {
            var service = new MainPageService(_dbContext, _mapper);

            int freeMonth = month + 1;
            var newPage = await service.CreatePageByParams(userId, year,freeMonth);

            Assert.True(newPage != null);
            Assert.NotEqual(0, newPage.Id);
            Assert.Equal(userId, newPage.UserID);
            Assert.Equal(year, newPage.Year);
            Assert.Equal(freeMonth, newPage.Month);
            

            Assert.NotNull(newPage.ImportantEvents);
            Assert.NotEqual(0, newPage.ImportantEvents.Id);
            Assert.NotNull(newPage.ImportantEvents.ImportantEvents);
            Assert.NotEqual(0, newPage.ImportantEvents.ImportantEvents.Id);

            Assert.NotNull(newPage.ImportantThings);
            Assert.NotEqual(0, newPage.ImportantThings.Id);
            Assert.NotNull(newPage.ImportantThings.ImportantThings);
            Assert.NotEqual(0, newPage.ImportantThings.ImportantThings.Id);
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
