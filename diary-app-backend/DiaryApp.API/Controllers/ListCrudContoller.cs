using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models.Lists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class ListCrudContoller<TList, TItem, TModel, UModel> : ControllerBase, IListCrudController<TModel, UModel> 
        where TList : ListBase<TItem>
        where TItem : ListItemBase
        where TModel : ListModel<UModel>
        where UModel : ListItemModel
    {
        internal IListService<TList, TItem> ListItemService { get; }
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ListCrudContoller(IListService<TList, TItem> listItemService, IMapper mapper, ILogger logger)
        {
            this.ListItemService = listItemService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public virtual async Task<IActionResult> AddList([FromBody] TModel eventListModel)
        {
            var eventList = mapper.Map<TList>(eventListModel);
            await ListItemService.Create(eventList);
            return Ok(eventList.ID);
        }

        public virtual async Task<IActionResult> AddItem([FromBody] UModel eventData)
        {
            try
            {
                var newEvent = mapper.Map<TItem>(eventData);
                await ListItemService.AddItem(newEvent, eventData.OwnerID);
                eventData.ID = newEvent.ID;
                return Ok(newEvent.ID);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        public virtual async Task<IActionResult> UpdateItem([FromBody] UModel eventModel)
        {
            var _event = mapper.Map<TItem>(eventModel);
            await ListItemService.UpdateItem(_event);
            return NoContent();
        }

        public virtual async Task<IActionResult> UpdateList([FromBody] TModel eventListModel)
        {
            var eventListToUpdate = await ListItemService.GetById(eventListModel.ID);
            if (eventListToUpdate == null)
                return NotFound();
            try
            {
                var _eventList = mapper.Map(eventListModel, eventListToUpdate);
                await ListItemService.Update(_eventList);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        public virtual async Task<IActionResult> DeleteItem(int eventID)
        {
            await ListItemService.DeleteItem(eventID);
            return NoContent();
        }

        public virtual async Task<IActionResult> DeleteList(int eventListID)
        {
            await ListItemService.Delete(eventListID);
            return NoContent();
        }
    }
}