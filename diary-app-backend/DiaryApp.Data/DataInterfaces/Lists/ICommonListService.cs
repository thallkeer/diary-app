using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;

namespace DiaryApp.Data.DataInterfaces
{
    public interface ICommonListService : ICrudService<CommonListDto, CommonList>
    {
    }
}
