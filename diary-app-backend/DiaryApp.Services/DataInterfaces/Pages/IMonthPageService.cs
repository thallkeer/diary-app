using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using System.Threading.Tasks;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IMonthPageService : IPageService<MonthPageDto, MonthPage>
    {       
        /// <summary>
        /// Transfer lists and areas of given page to the next month according to the transfer model.
        /// </summary>
        /// <param name="monthPageId"></param>
        /// <param name="transferDataSettings"></param>
        /// <returns></returns>
        Task TransferPageDataToNextMonthAsync(int monthPageId, PageAreaTransferSettingsDto transferDataSettings);
    }
}
