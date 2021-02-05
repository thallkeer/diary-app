using AutoMapper;
using DiaryApp.Core.Entities;
using DiaryApp.Models.DTO;
using DiaryApp.Data.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    public class PurchaseListsController : CrudController<PurchaseListDto, PurchaseList>
    {
        public PurchaseListsController(ICrudService<PurchaseListDto, PurchaseList> purchaseListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(purchaseListService, mapper, loggerFactory)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<ActionResult<PurchaseListDto>> GetAsync(int id)
        {
            var purchaseList = await _crudService.FirstOrDefaultAsync(pl => pl.Id == id);
            return Ok(mapper.Map<PurchaseListDto>(purchaseList));                     
        }
    }
}
