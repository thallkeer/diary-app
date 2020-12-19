using DiaryApp.Data.ServiceInterfaces;
using DiaryApp.Core.Models;
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DiaryApp.Core.DTO;
using DiaryApp.Core;

namespace DiaryApp.Tests
{
    public class MonthPageServiceTests : BaseTests
    {
        private IMonthPageService GetMonthPageService() => GetService<IMonthPageService>();

        [Fact]
        public async void TransferPageDataToNextMonthShouldCreateNextPageIfNotExists()
        {
            var service = GetMonthPageService();
            var prevMonthPage = await service.GetPageAsync(6, 2020, 6);
            var nextMonthPage = await service.GetPageAsync(6, 2020, 7);
            Assert.Null(nextMonthPage);
            var transferModel = TransferDataModel.CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(prevMonthPage, transferModel);
            nextMonthPage = await service.GetPageAsync(6, 2020, 7);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        [Fact]
        public async void TransferPageDataToNextMonthShouldWorkRight()
        {
            var service = GetMonthPageService();
            var prevMonthPage = await service.GetPageAsync(5, 2020, 5);
            var nextMonthPage = await service.GetPageAsync(6, 2020, 7);
            Assert.Null(nextMonthPage);
            var transferModel = TransferDataModel.CreateFullTransferModel();
            await service.TransferPageDataToNextMonthAsync(prevMonthPage, transferModel);
            nextMonthPage = await service.GetPageAsync(6, 2020, 7);
            Assert.NotNull(nextMonthPage);
            Assert.NotEqual(0, nextMonthPage.Id);
        }

        #region TestData
        

        #endregion
    }
}
