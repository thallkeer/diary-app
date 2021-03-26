using DiaryApp.Services.DataInterfaces;
using Xunit;
using DiaryApp.Services.DTO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.IntegrationTests
{
    public class MonthPageServiceTests : BaseTests
    {
        private IMonthPageService GetMonthPageService() => GetService<IMonthPageService>();

        [Fact]
        public async Task TransferPageDataToNextMonthShouldCreateNextPageIfNotExists()
        {
            var service = GetMonthPageService();
            var monthPage = await _dbContext.MonthPages.FirstAsync(p => p.Month < 11);
            await service.CreateAsync(monthPage.UserId, monthPage.Year, monthPage.Month);
            var nextMonthPage = await service.GetPageAsync(monthPage.UserId, monthPage.Year, monthPage.Month + 1);
            Assert.Null(nextMonthPage);
            var transferModel = CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(monthPage.Id, transferModel);
            nextMonthPage = await service.GetPageAsync(monthPage.UserId, monthPage.Year, monthPage.Month + 1);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        [Fact]
        public async Task TransferPageDataToNextMonthShouldCreateNextPageIfNotExists2()
        {
            var service = GetMonthPageService();
            var monthPage = await _dbContext.MonthPages.FirstAsync(p => p.Month < 11);
            var nextMonthPage = await service.GetPageAsync(monthPage.UserId, monthPage.Year, monthPage.Month + 1);
            Assert.Null(nextMonthPage);
            var transferModel = CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(monthPage.Id, transferModel);
            nextMonthPage = await service.GetPageAsync(monthPage.UserId, monthPage.Year, monthPage.Month + 1);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        [Theory]
        [MemberData(nameof(PageAreaTransferSettings))]
        public async Task TransferPageDataToNextMonthShouldWorkRight(PageAreaTransferSettingsDto transferModel, int userId, int month)
        {
            var service = GetMonthPageService();
            var monthPage = await service.GetPageAsync(userId, 2020, month);
            if (monthPage == null)
            {
                await service.CreateAsync(userId, 2020, month);
                monthPage = await service.GetPageAsync(userId, 2020, month);
            }
            Assert.NotNull(monthPage);
            Assert.NotEqual(0, monthPage.Id);

            //var pageAreasBefore = await GetPageAreas(service, monthPage);
            //Assert.All(pageAreasBefore, Assert.NotNull);

            await service.TransferPageDataToNextMonthAsync(monthPage.Id, transferModel);

            //nextMonthPage = await service.GetPageAsync(userId, 2020, month + 1);
            //List<MonthPageArea> pageAreaDtosAfter = await GetPageAreas(service, nextMonthPage);

            //foreach (var paType in transferModel.GetPresentAreaTypes())
            //{
            //    bool isAreaTransferred = transferModel.GetValueForArea(paType);
            //    var areaBefore = pageAreaDtos.First(pa => pa.GetType() == paType);
            //    var areaAfter = pageAreaDtosAfter.First(pa => pa.GetType() == paType);
            //    if (isAreaTransferred)
            //        Assert.NotEqual(areaBefore, areaAfter);
            //    else
            //        Assert.Equal(areaBefore, areaAfter);
            //}
        }

        #region Utils

        // private async Task<List<MonthPageArea>> GetPageAreas(IMonthPageService service, MonthPageDto nextMonthPage)
        // {
        //     var ga = await GetPageArea<GoalsAreaDto, GoalsArea>(nextMonthPage.Id);
        //     var pa = await GetPageArea<PurchasesAreaDto, PurchasesArea>(nextMonthPage.Id);
        //     var ia = await GetPageArea<IdeasAreaDto, IdeasArea>(nextMonthPage.Id);
        //     var da = await GetPageArea<DesiresAreaDto, DesiresArea>(nextMonthPage.Id);
        //     List<MonthPageArea> pageAreas = new List<MonthPageArea> { ga, pa, ia, da };
        //     return pageAreas;
        // }
        //
        // private async Task<TArea> GetPageArea<TAreaDto, TArea>(int pageId)
        //      where TAreaDto : PageAreaDto
        //      where TArea : MonthPageArea
        // {
        //     var service = GetMonthPageService();
        //     return await service.GetPageAreaOrThrowAsync<TArea, TAreaDto>(pageId);
        // }

        #endregion

        #region TestData
        public static TheoryData<PageAreaTransferSettingsDto, int, int> PageAreaTransferSettings()
        {
            var td = new TheoryData<PageAreaTransferSettingsDto, int, int>
            {
                { CreateFullTransferModel(), 6, 6 },
                { new PageAreaTransferSettingsDto { TransferPurchasesArea = true }, 6, 7 },
                { new PageAreaTransferSettingsDto { TransferDesiresArea = true }, 6, 8 },
                { new PageAreaTransferSettingsDto { TransferGoalsArea = true }, 6, 9 },
                { new PageAreaTransferSettingsDto { TransferIdeasArea = true }, 6, 10 },
                { new PageAreaTransferSettingsDto { TransferIdeasArea = true, TransferPurchasesArea = true }, 6, 11 },
                { new PageAreaTransferSettingsDto(), 3, 4 }
            };
            return td;
        }

        public static PageAreaTransferSettingsDto CreateFullTransferModel()
        {
            return new()
            {
                TransferDesiresArea = true,
                TransferGoalsArea = true,
                TransferIdeasArea = true,
                TransferPurchasesArea = true
            };
        }
        #endregion
    }
}
