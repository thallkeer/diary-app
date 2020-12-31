using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPageDto, MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }

        public async Task TransferPageDataToNextMonthAsync(int monthPageId, TransferDataModel transferDataModel)
        {
            if (transferDataModel == null) throw new ArgumentNullException(nameof(transferDataModel));

            MonthPage monthPage = await _dbSet.FindAsync(monthPageId);
            if (monthPage == null)
                throw new EntityNotFoundException(nameof(MonthPage));

            int nextMonth = monthPage.Month + 1;
            MonthPage nextPage = await GetPageAsync(monthPage.UserId, monthPage.Year, nextMonth);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (nextPage == null)
                {
                    nextPage = monthPage.TransferDataToNextMonth(transferDataModel);
                    await _dbSet.AddAsync(nextPage);
                }
                else
                {
                    nextPage.MergePageAreas(transferDataModel, monthPage);
                    _dbSet.Update(nextPage);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new PageDataTransferException("Could not transfer page data to the next month, exception is occured.", ex);
            }
        }
    }
}
