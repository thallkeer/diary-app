using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IMonthPageService : IPageService<MonthPageDto, MonthPage>
    {       
        /// <summary>
        /// Transfer lists and areas of given page to the next month according to the transfer model.
        /// </summary>
        /// <param name="monthPageId"></param>
        /// <param name="transferDataModel"></param>
        /// <returns></returns>
        Task TransferPageDataToNextMonthAsync(int monthPageId, TransferDataModel transferDataModel);
    }
}
