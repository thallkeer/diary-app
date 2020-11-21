using DiaryApp.Core.Models;
using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface IMonthPageService : IPageService<MonthPage>
    {       
        Task TransferPageDataToNextMonth(MonthPage monthPage, TransferDataModel transferDataModel);
    }
}
