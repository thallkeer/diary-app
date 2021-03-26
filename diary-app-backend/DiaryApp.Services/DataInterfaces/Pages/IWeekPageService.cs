using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.Pages;
using DiaryApp.Services.DTO;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IWeekPageService : IPageService<WeekPageDto, WeekPage>
    {
    }
}
