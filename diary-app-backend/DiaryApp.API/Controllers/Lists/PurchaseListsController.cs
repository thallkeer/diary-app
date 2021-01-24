using AutoMapper;
using DiaryApp.Core.Models;
using DiaryApp.Data.DTO;
using DiaryApp.Data.ServiceInterfaces;
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
            var purchaseList = await _crudService.GetOneByCriteriaOrDefaultAsync(pl => pl.Id == id);
            return Ok(mapper.Map<PurchaseListDto>(purchaseList));                     
        }
    }
}
