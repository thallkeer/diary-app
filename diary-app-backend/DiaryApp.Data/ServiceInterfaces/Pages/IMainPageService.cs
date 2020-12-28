using DiaryApp.Core;
using DiaryApp.Core.DTO;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IMainPageService : IPageService<MainPageDto>, IGetable<MainPageDto, MainPage>
    {
    }
}
