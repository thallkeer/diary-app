using AutoMapper;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController<TDto, TEntity> : AppBaseController<CrudController<TDto, TEntity>>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        private readonly ICrudService<TDto, TEntity> _crudService;
        public CrudController(IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAsync([FromBody] TDto createModel)
        {
            var id = await _crudService.CreateAsync(createModel);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutAsync([FromBody] TDto updateModel)
        {
            await _crudService.UpdateAsync(updateModel);
            return Ok();
        }

        [HttpDelete("{eventListID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();   
        }
    }
}
