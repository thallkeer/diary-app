using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DataInterfaces.Pages;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IMainPageService : IPageService<MainPageDto, MainPage>
    {
    }
}
