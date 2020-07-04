using DiaryApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    public interface IListCrudController<TModel, UModel>
        where TModel : ListModel<UModel>
        where UModel : ListItemModel
    {
        IActionResult GetByPageID(int pageID);
        Task<IActionResult> AddItem([FromBody] UModel itemModel);
        Task<IActionResult> AddList([FromBody] TModel listModel);
        Task<IActionResult> DeleteItem(int itemID);
        Task<IActionResult> DeleteList(int listID);
        Task<IActionResult> UpdateItem([FromBody] UModel itemModel);
        Task<IActionResult> UpdateList([FromBody] TModel listModel);
    }
}