using DiaryApp.Services.Exceptions;
using DiaryApp.Services.DataInterfaces;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Services.DTO;

namespace DiaryApp.IntegrationTests
{
    public class MainPageServiceTests : BaseTests
    {
        private IMainPageService GetMainPageService() => GetService<IMainPageService>();

        [Theory]
        [InlineData(1, 2020, 1)]
        public async Task GetPageAsyncShouldReturnPageIfExists(int userId, int year, int month)
        {
            var service = GetMainPageService();

            await service.CreateAsync(userId, year, month);

            MainPageDto mainPage = await service.GetPageAsync(userId, year, month);

            Assert.True(mainPage != null);
            Assert.Equal(userId, mainPage.UserId);
            Assert.Equal(year, mainPage.Year);
            Assert.Equal(month, mainPage.Month);     
        }

        [Fact]
        public async Task GetImportantEventsAreaShouldReturnArea()
        {
            var service = GetMainPageService();

            var page = await service.CreateAsync(1, 2020, 6);

            Assert.NotNull(page);

            var pageArea = await service.GetPageAreaOrThrowAsync<ImportantEventsArea>(page.Id);

            Assert.NotNull(pageArea);
            Assert.NotEqual(0, pageArea.Id);
            Assert.NotNull(pageArea.ImportantEvents);
            Assert.NotNull(pageArea.ImportantEvents.Items);
        }

        [Fact]
        public async Task CreatePageByParamsShouldFailIfPageExists()
        {
            var service = GetMainPageService();

            int year = 2020;
            int month = 11;
            var page = await service.CreateAsync(1, year, month);

            Assert.NotNull(page);

            await Assert.ThrowsAsync<PageAlreadyExistsException>(async () => await service.CreateAsync(page.UserId, year, month));
        }

        [Fact]
        public async Task CreatePageShouldFailIfUserDoesNotExists()
        {
            var service = GetMainPageService();

            await Assert.ThrowsAsync<DbUpdateException>(async () => await service.CreateAsync(666, 2020, 2));
        }

        [Theory]
        [InlineData(1, 2020, 1)]
        [InlineData(1, 2020, 5)]
        public async Task CreatePageByParamsShouldCreatePage(int userId, int year, int month)
        {
            var service = GetMainPageService();

            await service.CreateAsync(userId, year, month);

            var newPage = await service.GetPageAsync(userId, year, month);

            Assert.True(newPage != null);
            Assert.NotEqual(0, newPage.Id);
            Assert.Equal(userId, newPage.UserId);
            Assert.Equal(year, newPage.Year);
            Assert.Equal(month, newPage.Month);

            //TODO: decide what to do with these properties
            //Assert.NotNull(newPage.ImportantEventsArea);
            //Assert.NotEqual(0, newPage.ImportantEventsArea.Id);
            //Assert.NotNull(newPage.ImportantEventsArea.ImportantEvents);
            //Assert.NotEqual(0, newPage.ImportantEventsArea.ImportantEvents.Id);

            //Assert.NotNull(newPage.ImportantThingsArea);
            //Assert.NotEqual(0, newPage.ImportantThingsArea.Id);
            //Assert.NotNull(newPage.ImportantThingsArea.ImportantThings);
            //Assert.NotEqual(0, newPage.ImportantThingsArea.ImportantThings.Id);
        }        
    }
}
