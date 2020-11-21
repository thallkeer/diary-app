using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommonListController : AppBaseController<CommonListController>
    {
        private readonly ListCrudContoller<CommonList, ListItem, CommonListModel, ListItemModel> crudController;

        public CommonListController(ICommonListService commonListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.crudController = new ListCrudContoller<CommonList, ListItem, CommonListModel, ListItemModel>(commonListService, mapper, logger);
        }

        [HttpPost]
        public async Task<IActionResult> AddCommonList([FromBody] CommonListModel CommonListModel)
        {
            return await crudController.AddList(CommonListModel);
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddEvent([FromBody] ListItemModel eventModel)
        {
            return await crudController.AddItem(eventModel);
        }

        [HttpPut("updateItem")]
        public async Task<IActionResult> UpdateEvent([FromBody] ListItemModel eventModel)
        {
            return await crudController.UpdateItem(eventModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCommonList([FromBody] CommonListModel CommonListModel)
        {
            return await crudController.UpdateList(CommonListModel);
        }

        [HttpDelete("deleteItem/{itemID}")]
        public async Task DeleteEvent(int itemID)
        {
            await crudController.DeleteItem(itemID);
        }

        [HttpDelete("{commonListID}")]
        public async Task DeleteCommonList(int commonListID)
        {
            await crudController.DeleteList(commonListID);
        }
    }
}