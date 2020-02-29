using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using System;

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

        public async Task TransferPageDataToNextMonth(MonthPage prevPage, TransferDataModel transferDataModel)
        {
            MonthPage monthPage = await GetPageForUser(prevPage.UserID, prevPage.Year, prevPage.Month + 1);
            if (monthPage == null)
            {
                monthPage = new MonthPage(prevPage.Year, prevPage.Month + 1, prevPage.User);
                await Create(monthPage);
            }

            if (transferDataModel.TransferGoalsArea)
            {
                monthPage.GoalsArea = TransferArea(prevPage.GoalsArea, monthPage);
            }
            if (transferDataModel.TransferDesiresArea)
            {
                monthPage.DesiresArea = TransferArea(prevPage.DesiresArea, monthPage);
            }
            if (transferDataModel.TransferIdeasArea)
            {
                monthPage.IdeasArea = TransferArea(prevPage.IdeasArea, monthPage);
            }
            if (transferDataModel.TransferPurchasesArea)
            {
                monthPage.PurchasesArea = TransferArea(prevPage.PurchasesArea, monthPage);
            }

            await Update(monthPage);
        }

        private T TransferArea<T>(T prevArea, MonthPage monthPage) where T : PageAreaBase
        {
            return (T) prevArea?.TransferAreaData(monthPage);
        }
    }
}
