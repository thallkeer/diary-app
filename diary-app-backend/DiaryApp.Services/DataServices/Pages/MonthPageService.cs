using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Core.Entities.Users.Settings;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Users;
using DiaryApp.Services.DTO;
using DiaryApp.Services.Exceptions;

namespace DiaryApp.Services.DataServices.Pages
{
    public class MonthPageService : PageService<MonthPageDto, MonthPage>, IMonthPageService
    {
        public MonthPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {}

        public override async Task<MonthPageDto> CreateAsync(int userId, int year, int month)
        {
            var settings = await userService.GetSettingsAsync(userId);
            if (settings?.PageAreaTransferSettings == null)
                return await base.CreateAsync(userId, year, month);

            var prevMonth = month - 1;
            var prevPage = await GetPageAsync(userId, year, prevMonth);
            if (prevPage == null)
                return await base.CreateAsync(userId, year, month);

            await TransferPageDataToNextMonthAsync(prevPage.Id, settings.PageAreaTransferSettings);
            //TODO catch and handle PageDataTransferException
            return await GetPageAsync(userId, year, month);
        }

        public async Task TransferPageDataToNextMonthAsync(int monthPageId, PageAreaTransferSettingsDto transferSettings)
        {
            Guard.Against.Null(transferSettings, nameof(transferSettings));

            var monthPage = await _dbSet.FindAsync(monthPageId);
            if (monthPage == null)
                throw new EntityNotFoundException<MonthPage>();

            var nextMonth = monthPage.Month + 1;
            var nextPage = await GetPageEntityAsync(monthPage.UserId, monthPage.Year, nextMonth);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transferDataModel = _mapper.Map<PageAreaTransferSettings>(transferSettings);
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
                throw new PageDataTransferException(ex);
            }
        }
    }
}
