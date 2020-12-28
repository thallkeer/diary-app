using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.Models;
using Xunit;
using System.Collections.Generic;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Reflection;
using DiaryApp.Data.Extensions;
using DiaryApp.Core.Interfaces;

namespace DiaryApp.Tests
{
    public class MonthPageServiceTests : BaseTests
    {
        private IMonthPageService GetMonthPageService() => GetService<IMonthPageService>();

        [Theory]
        [InlineData(5, 6)]
        public async void TransferPageDataToNextMonthShouldCreateNextPageIfNotExists(int userId, int month)
        {
            var service = GetMonthPageService();
            var prevMonthPage = await service.GetPageAsync(userId, 2020, month);
            var nextMonthPage = await service.GetPageAsync(userId, 2020, month + 1);
            Assert.Null(nextMonthPage);
            var transferModel = TransferDataModel.CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(prevMonthPage, transferModel);
            nextMonthPage = await service.GetPageAsync(userId, 2020, month + 1);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        [Theory]
        [MemberData(nameof(TransferDataModels))]
        public async void TransferPageDataToNextMonthShouldWorkRight(TransferDataModel transferModel, int userId, int month)
        {
            var service = GetMonthPageService();
            var prevMonthPage = await service.GetPageAsync(userId, 2020, month);

            ///emulates that next page already exists
            var nextMonthPage = await service.CreateAsync(userId, 2020, month + 1);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
            List<PageAreaBase<MonthPage>> pageAreaDtos = await GetPageAreas(service, nextMonthPage);
            Assert.All(pageAreaDtos, (parea) => Assert.NotNull(parea));

            await service.TransferPageDataToNextMonthAsync(prevMonthPage, transferModel);
            nextMonthPage = await service.GetPageAsync(userId, 2020, month + 1);
            List<PageAreaBase<MonthPage>> pageAreaDtosAfter = await GetPageAreas(service, nextMonthPage);

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

        private async Task<List<PageAreaBase<MonthPage>>> GetPageAreas(IMonthPageService service, MonthPageDto nextMonthPage)
        {
            var ga = await GetPageArea<GoalsAreaDto, GoalsArea>(nextMonthPage.Id);
            var pa = await GetPageArea<PurchasesAreaDto, PurchasesArea>(nextMonthPage.Id);
            var ia = await GetPageArea<IdeasAreaDto, IdeasArea>(nextMonthPage.Id);
            var da = await GetPageArea<DesiresAreaDto, DesiresArea>(nextMonthPage.Id);
            List<PageAreaBase<MonthPage>> pageAreas = new List<PageAreaBase<MonthPage>> { ga, pa, ia, da };
            return pageAreas;
        }

        private async Task<TArea> GetPageArea<TAreaDto, TArea>(int pageId)
             where TAreaDto : PageAreaDto
             where TArea : class, IPageArea
        {
            var service = GetMonthPageService();
            var dto = await service.GetPageArea<TAreaDto, TArea>(pageId);
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
