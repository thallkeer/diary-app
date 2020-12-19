using DiaryApp.Core.DTO;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Data.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DiaryApp.Tests
{
    public class MainPageServiceTests : BaseTests
    {
        private IMainPageService GetMainPageService() => GetService<IMainPageService>(); // new MainPageService(_dbContext, _mapper, GetService<IUserService>());

        [Theory]
        [InlineData(1, 2020, 1)]
        [InlineData(1, 2020, 12)]
        [InlineData(12, 2020, 12)]
        [InlineData(2, 2020, 2)]
        [InlineData(2, 2020, 1)]
        public async Task GetPageAsyncShouldReturnPageIfExists(int userId, int year, int month)
        {
            MainPageDto mainPage = await GetMainPageService()
                                        .GetPageAsync(userId, year, month);

            Assert.True(mainPage != null);
            Assert.Equal(userId, mainPage.UserID);
            Assert.Equal(year, mainPage.Year);
            Assert.Equal(month, mainPage.Month);     
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

        [Theory]
        [InlineData(1, 2020, 1)]
        [InlineData(2, 2020, 2)]
        [InlineData(12, 2020, 12)]
        public async Task CreatePageByParamsShouldFailIfPageExists(int userId, int year, int month)
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<PageAlreadyExistsException>(async () => await service.CreateAsync(userId, year, month));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfMonthValueIsWrong(int inputMonth)
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreateAsync(1, 2020, inputMonth));
        }

        [Theory]
        [MemberData(nameof(WrongMonthAndYearValues))]
        public async Task CreatePageByParamsShouldFailIfYearValueIsWrong(int inputYear)
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await service.CreateAsync(1, inputYear, 3));
        }

        [Fact]
        public async Task CreatePageShouldFailIfUserDoesNotExists()
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<UserNotExistsException>(async () => await service.CreateAsync(50, 2020, 2));
        }

        [Theory]
        [InlineData(11, 2020, 2)]
        [InlineData(11, 2020, 3)]
        [InlineData(12, 2020, 5)]
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
