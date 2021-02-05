using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;

namespace DiaryApp.Data.DataInterfaces.Lists
{
    public interface ICommonListItemService : ICrudService<ListItemDto, ListItem>
    {
    }
}
