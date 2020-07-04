using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using System;
using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPage>, IMonthPageService
    {        
        public MonthPageService(ApplicationContext context) : base(context)
        {
        }

        public async Task<T> GetPageArea<T>(int pageID) where T : PageAreaBase
        {
            return await context.Set<T>().FirstOrDefaultAsync(area => area.PageID == pageID);            
        }

        public override async Task<MonthPage> CreatePageByParams(int userID, int year, int month)
        {
            var monthPage = new MonthPage()
            {
                UserID = userID,
                Year = year,
                Month = month
            };

            await Create(monthPage);

            monthPage.CreateAreas();

            await Update(monthPage);
           
            return monthPage;
        }

        public async Task TransferPageDataToNextMonth(MonthPage prevPage, TransferDataModel transferDataModel)
        {
            MonthPage monthPage = await GetPageForUser(prevPage.UserID, prevPage.Year, prevPage.Month + 1);
            if (monthPage == null)
            {
                monthPage = new MonthPage(prevPage.Year, prevPage.Month + 1, prevPage.User);
                await Create(monthPage);
            }

            var transferOperations = new TransferAreaOperations(context, monthPage, transferDataModel);
            
            monthPage.GoalsArea = await transferOperations.InitOrTransferArea(monthPage.GoalsArea, prevPage.GoalsArea);
            monthPage.DesiresArea = await transferOperations.InitOrTransferArea(monthPage.DesiresArea, prevPage.DesiresArea);
            monthPage.IdeasArea = await transferOperations.InitOrTransferArea(monthPage.IdeasArea, prevPage.IdeasArea);
            monthPage.PurchasesArea = await transferOperations.InitOrTransferArea(monthPage.PurchasesArea, prevPage.PurchasesArea);

            await Update(monthPage);
        }

        private class TransferAreaOperations
        {           
            private readonly ApplicationContext context;
            private readonly MonthPage monthPage;
            private readonly TransferDataModel transferDataModel;

            private PageAreaFactoryCreator factoryCreator;
            private PageAreaFactoryCreator FactoryCreator
            {
                get
                {
                    if (factoryCreator == null)
                        factoryCreator = new PageAreaFactoryCreator();
                    return factoryCreator;
                }
            }
            

            public TransferAreaOperations(ApplicationContext context, MonthPage monthPage, TransferDataModel transferDataModel)
            {
                this.context = context;
                this.monthPage = monthPage;
                this.transferDataModel = transferDataModel;
            }

            public async Task<T> InitOrTransferArea<T>(T area, T prevArea) where T : PageAreaBase
            {
                bool transfer = transferDataModel.GetValueForArea(prevArea.AreaType);
                if (area == null)
                {
                    if (transfer)
                        area = (T)prevArea.TransferAreaData(monthPage);
                    else
                    {
                        IPageAreaFactory pageAreaFactory = FactoryCreator.GetFactoryByAreaType(prevArea.AreaType);
                        area = (T)pageAreaFactory.CreatePageArea(monthPage);
                    }

                    context.Set<T>().Add(area);
                    await context.SaveChangesAsync();
                }
                else if (transfer)
                {
                    area.AddFromOtherArea(prevArea);
                    context.Set<T>().Update(area);
                    await context.SaveChangesAsync();
                }
                return area;
            }
        }
    }
}
