using System.Threading;
using System.Threading.Tasks;
using DiaryApp.API.Requests;
using DiaryApp.Core.Entities.ListWrappers;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DataInterfaces.Lists;
using DiaryApp.Services.DTO.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers.Lists
{
    public class PurchaseListsController : DiaryAppController
    {
        private readonly IPurchaseListService _purchaseListService;

        public PurchaseListsController(IPurchaseListService purchaseListService)
        {
            _purchaseListService = purchaseListService;
        }

        /// <summary>
        /// Creates new purchase list for given purchase area from todolist model.
        /// </summary>
        /// <param name="requestModel">Parameters to create purchase list</param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateAsync([FromBody] CreatePurchaseListRequest requestModel, CancellationToken cancellationToken = default)
        {
            var purchaseListId = await _purchaseListService.CreateAsync(requestModel.TodoList, requestModel.PurchasesAreaId);            
            return Ok(purchaseListId);
        }

        [HttpGet("{purchaseListId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetTodoListIdAsync(int purchaseListId, CancellationToken cancellationToken = default)
        {
            var todoListId = await _purchaseListService.GetTodoListId(purchaseListId);
            return Ok(todoListId);
        }
    }
}
