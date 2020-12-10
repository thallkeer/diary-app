using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models.PageAreas;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IPageAreaService<TPageAreaDto, TPageArea, TPage>
       where TPageAreaDto : PageAreaDto
       where TPageArea : PageAreaBase<TPage>
       where TPage : PageBase
    {
        Task<TPageAreaDto> GetPageAreaByPage(int pageID);
    }
}
