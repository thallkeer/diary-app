using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.DataInterfaces;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPageDto, MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {}

        //TODO: write tests
        public override async Task<MonthPageDto> CreateAsync(int userId, int year, int month)
        {
            var settings = await userService.GetSettingsAsync(userId);
            if (settings == null || settings.PageAreaTransferSettings == null)
                return await base.CreateAsync(userId, year, month);

            int prevMonth = month - 1;
            var prevPage = await GetPageAsync(userId, year, prevMonth);
            if (prevPage == null)
                return await base.CreateAsync(userId, year, month);

            var transferSettings = settings.PageAreaTransferSettings;
            var transferModel = new TransferDataModel
            {
                TransferPurchasesArea = transferSettings.TransferPurchasesArea,
                TransferDesiresArea = transferSettings.TransferDesiresArea,
                TransferIdeasArea = transferSettings.TransferIdeasArea,
                TransferGoalsArea = transferSettings.TransferGoalsArea
            };

            await TransferPageDataToNextMonthAsync(prevPage.Id, transferModel);
            //TODO catch and handle PageDataTransferException
            return await GetPageAsync(userId, year, month);
        }

        public async Task TransferPageDataToNextMonthAsync(int monthPageId, TransferDataModel transferDataModel)
        {
            if (transferDataModel == null) throw new ArgumentNullException(nameof(transferDataModel));

            MonthPage monthPage = await _dbSet.FindAsync(monthPageId);
            if (monthPage == null)
                throw new EntityNotFoundException(nameof(MonthPage));

            int nextMonth = monthPage.Month + 1;
            MonthPage nextPage = await GetPageEntityAsync(monthPage.UserId, monthPage.Year, nextMonth);

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
