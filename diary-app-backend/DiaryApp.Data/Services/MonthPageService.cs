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

            //monthPage.PurchasesArea.PurchasesLists.AddRange(new TodoList[]
            //{
            //    new TodoList {Title = "Название списка"},
            //    new TodoList {Title = "Название списка"}
            //});

            //monthPage.PurchasesArea.PurchasesLists.ForEach(x => x.Page = monthPage);

            //monthPage.DesiresArea.DesiresLists.AddRange(new EventList[]
            //{
            //    new EventList {Title = "Прочитать"},
            //    new EventList {Title = "Посмотреть"},
            //    new EventList {Title = "Посетить"},
            //});

            //monthPage.DesiresArea.DesiresLists.ForEach(x => x.Page = monthPage);

            //monthPage.IdeasArea.IdeasList = new EventList() { Page = monthPage };
            //monthPage.GoalsArea.GoalsLists.Add(new HabitsTracker() { GoalName = "Название цели" });

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

            monthPage.GoalsArea = await InitOrTransferArea(monthPage.GoalsArea, prevPage.GoalsArea, monthPage,transferDataModel.TransferGoalsArea, () => new GoalsArea(monthPage, true));
            monthPage.DesiresArea = await InitOrTransferArea(monthPage.DesiresArea, prevPage.DesiresArea, monthPage, transferDataModel.TransferDesiresArea, () => new DesiresArea(monthPage, true));
            monthPage.IdeasArea = await InitOrTransferArea(monthPage.IdeasArea, prevPage.IdeasArea, monthPage, transferDataModel.TransferIdeasArea, () => new IdeasArea(monthPage, true));
            monthPage.PurchasesArea = await InitOrTransferArea(monthPage.PurchasesArea, prevPage.PurchasesArea, monthPage, transferDataModel.TransferPurchasesArea, () => new PurchasesArea(monthPage, true));

            await Update(monthPage);
        }

        private async Task<T> InitOrTransferArea<T>(T area, T prevArea, MonthPage monthPage, bool transfer, Func<T> areaCreator) where T : PageAreaBase
        {
            if (area == null)
            {
                if (transfer)
                    area = (T)prevArea.TransferAreaData(monthPage);
                else
                    area = areaCreator();

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
