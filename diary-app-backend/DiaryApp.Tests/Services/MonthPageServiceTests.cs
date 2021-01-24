using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.Models;
using Xunit;
using System.Collections.Generic;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.DTO;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Tests
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
            var transferModel = TransferDataModel.CreateFullTransferModel();
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
            var transferModel = TransferDataModel.CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(monthPage.Id, transferModel);
            nextMonthPage = await service.GetPageAsync(monthPage.UserId, monthPage.Year, monthPage.Month + 1);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        [Theory]
        [MemberData(nameof(TransferDataModels))]
        public async Task TransferPageDataToNextMonthShouldWorkRight(TransferDataModel transferModel, int userId, int month)
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

            List<MonthPageArea> pageAreasBefore = await GetPageAreas(service, monthPage);
            Assert.All(pageAreasBefore, (parea) => Assert.NotNull(parea));

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

        private async Task<List<MonthPageArea>> GetPageAreas(IMonthPageService service, MonthPage nextMonthPage)
        {
            var ga = await GetPageArea<GoalsAreaDto, GoalsArea>(nextMonthPage.Id);
            var pa = await GetPageArea<PurchasesAreaDto, PurchasesArea>(nextMonthPage.Id);
            var ia = await GetPageArea<IdeasAreaDto, IdeasArea>(nextMonthPage.Id);
            var da = await GetPageArea<DesiresAreaDto, DesiresArea>(nextMonthPage.Id);
            List<MonthPageArea> pageAreas = new List<MonthPageArea> { ga, pa, ia, da };
            return pageAreas;
        }

        private async Task<TArea> GetPageArea<TAreaDto, TArea>(int pageId)
             where TAreaDto : PageAreaDto
             where TArea : class, IPageArea
        {
            var service = GetMonthPageService();
            var dto = await service.GetPageArea<TArea>(pageId);
            return _mapper.Map<TArea>(dto);
        }

        #endregion

        #region TestData
        public static TheoryData<TransferDataModel, int, int> TransferDataModels()
        {
            var td = new TheoryData<TransferDataModel, int, int>
            {
                { TransferDataModel.CreateFullTransferModel(), 6, 6 },
                { new TransferDataModel { TransferPurchasesArea = true }, 6, 7 },
                { new TransferDataModel { TransferDesiresArea = true }, 6, 8 },
                { new TransferDataModel { TransferGoalsArea = true }, 6, 9 },
                { new TransferDataModel { TransferIdeasArea = true }, 6, 10 },
                { new TransferDataModel { TransferIdeasArea = true, TransferPurchasesArea = true }, 6, 11 },
                { new TransferDataModel(), 3, 4 }
            };
            return td;
        }
        #endregion
    }
}

public static class TransferDataModelExtensions
{
    public static IEnumerable<Type> GetPresentAreaTypes(this TransferDataModel transferDataModel)
    {
        return transferDataModel
            .GetType()
            .GetProperties()
            .Select(p => p.GetCustomAttribute<PageAreaAttribute>())
            .Select(a => a.AreaType);
    }
}
