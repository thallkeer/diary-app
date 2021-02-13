using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces.Lists
{
    public interface IEventItemService : ICrudService<EventItemDto, EventItem>
    {
        Task<int> CreateWithNotificationAsync(EventItemDto dto, int userId);
    }
}
