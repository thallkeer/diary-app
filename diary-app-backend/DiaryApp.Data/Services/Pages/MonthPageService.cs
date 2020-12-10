using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;

namespace DiaryApp.Data.Services
{
    public class MonthPageService : PageService<MonthPageDto, MonthPage>, IMonthPageService
    {        
        public MonthPageService(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task TransferPageDataToNextMonth(MonthPageDto prevPageDto, TransferDataModel transferDataModel)
        {
            MonthPageDto monthPageDto = await GetPageForUser(prevPageDto.UserID, prevPageDto.Year, prevPageDto.Month + 1);

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                if (monthPageDto == null)
                {
                    monthPageDto = await CreatePageByParams(prevPageDto.UserID, prevPageDto.Year, prevPageDto.Month + 1);
                }

                var monthPage = monthPageDto.ToEntity<MonthPage, MonthPageDto>(mapper);

                var transferOperations = new TransferAreaOperations(context, monthPage, transferDataModel);

                var prevPage = prevPageDto.ToEntity<MonthPage, MonthPageDto>(mapper);

                monthPage.GoalsArea = await transferOperations.InitOrTransferArea(monthPage.GoalsArea, prevPage.GoalsArea);
                monthPage.DesiresArea = await transferOperations.InitOrTransferArea(monthPage.DesiresArea, prevPage.DesiresArea);
                monthPage.IdeasArea = await transferOperations.InitOrTransferArea(monthPage.IdeasArea, prevPage.IdeasArea);
                monthPage.PurchasesArea = await transferOperations.InitOrTransferArea(monthPage.PurchasesArea, prevPage.PurchasesArea);

                context.Update(monthPage);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
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

            public async Task<T> InitOrTransferArea<T>(T area, T prevArea) where T : PageAreaBase<MonthPage>, IMonthPageArea<T>
            {
                bool transfer = transferDataModel.GetValueForArea(prevArea.AreaType);
                if (area == null)
                {
                    if (transfer)
                        area = prevArea.TransferAreaData(monthPage);
                    else
                    {
                        IPageAreaFactory pageAreaFactory = FactoryCreator.GetFactoryByAreaType(prevArea.AreaType);
                        area = (T) pageAreaFactory.CreatePageArea(monthPage);
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
