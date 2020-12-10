using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IMonthPageService : IPageService<MonthPageDto>
    {       
        /// <summary>
        /// Transfer lists and areas of given page to the next month according to the transfer model.
        /// </summary>
        /// <param name="monthPage"></param>
        /// <param name="transferDataModel"></param>
        /// <returns></returns>
        Task TransferPageDataToNextMonth(MonthPageDto monthPage, TransferDataModel transferDataModel);
    }
}
