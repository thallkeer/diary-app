using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DiaryApp.Tests
{
    public class MainPageServiceTests : BaseTests
    {
        const int userTestId = 1;
        const int testYear = 2020;
        const int testMonth = 10;

        private MainPageService GetMainPageService() => new MainPageService(_dbContext, _mapper, null);

        [Fact]
        public async Task ShouldReturnPageIfExists()
        {
            // arrange
            int pageId = 2;

            MainPageDto mainPage = await GetMainPageService()
                                        .GetPageAsync(userTestId, testYear, testMonth);

            // assert
            Assert.True(mainPage != null);
            Assert.Equal(pageId, mainPage.Id);
            Assert.Equal(userTestId, mainPage.UserID);
            Assert.Equal(testYear, mainPage.Year);
            Assert.Equal(testMonth, mainPage.Month);     
        }

        [Fact]
        public async Task GetImportantEventsAreaShouldReturnArea()
        {
            var service = GetMainPageService();

            var pageArea = await service.GetPageArea<ImportantEventsAreaDto, ImportantEventsArea>(2);
            Assert.True(pageArea != null);
            Assert.NotEqual(0, pageArea.Id);
            Assert.NotNull(pageArea.ImportantEvents);
            Assert.NotNull(pageArea.ImportantEvents.Items);
        }

        [Fact]
        public async Task CreatePageByParamsShouldFailIfPageExists()
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<PageAlreadyExistsException>(async () => await service.CreateAsync(userTestId, testYear, testMonth));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfMonthValueIsWrong(int inputMonth)
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreateAsync(userTestId, testYear, inputMonth));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfYearValueIsWrong(int inputYear)
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreateAsync(userTestId, inputYear, testMonth));
        }

        public async Task CreatePageShouldFailIfUserDoesNotExists()
        {

        }

        [Theory]
        [InlineData(1, 2020, 2)]
        [InlineData(1, 2020, 3)]
        [InlineData(2, 2020, 5)]
        public async Task CreatePageByParamsShouldCreatePage(int userId, int year, int month)
        {
            var service = GetMainPageService();

            var newPage = await service.CreateAsync(userId, year, month);

            Assert.True(newPage != null);
            Assert.NotEqual(0, newPage.Id);
            Assert.Equal(userId, newPage.UserID);
            Assert.Equal(year, newPage.Year);
            Assert.Equal(month, newPage.Month);
            

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
