using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPageDto, MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }

        public async Task TransferPageDataToNextMonthAsync(MonthPageDto prevPageDto, TransferDataModel transferDataModel)
        {
            PageDto nextPageDto = new PageDto(prevPageDto.UserID, prevPageDto.Year, prevPageDto.Month + 1);

            MonthPageDto monthPageDto = await GetPageAsync(nextPageDto.UserID, nextPageDto.Year, nextPageDto.Month);

            //using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                monthPageDto ??= await CreateAsync(nextPageDto.UserID, nextPageDto.Year, nextPageDto.Month);

                var monthPage = await dbSet.FindAsync(monthPageDto.Id);

                var prevPage = await dbSet.FindAsync(prevPageDto.Id);

                monthPage.GoalsArea = TransferAreaData(transferDataModel, monthPage.GoalsArea, prevPage.GoalsArea);
                monthPage.DesiresArea = TransferAreaData(transferDataModel, monthPage.DesiresArea, prevPage.DesiresArea);
                monthPage.PurchasesArea = TransferAreaData(transferDataModel, monthPage.PurchasesArea, prevPage.PurchasesArea);
                monthPage.IdeasArea = TransferAreaData(transferDataModel, monthPage.IdeasArea, prevPage.IdeasArea);

                context.Update(monthPage);

                await context.SaveChangesAsync();

                // Commit transaction if all commands succeed, transaction will auto-rollback
                // when disposed if either commands fails

                //await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new PageDataTransferException("Could not transfer page data to the next month, exception is occured.", ex);
            }
        }

        private T TransferAreaData<T>(TransferDataModel transferDataModel, T area, T prevArea) where T : PageAreaBase<MonthPage>, IMonthPageArea<T>
        {
            bool transfer = transferDataModel.GetValueForArea(typeof(T));
            if (transfer)
            {
                area.AddFromOtherArea(prevArea);
                context.Set<T>().Update(area);
            }
            return area;
        }
    }
}
