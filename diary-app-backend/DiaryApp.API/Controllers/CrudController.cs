using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers
{
    /// <summary>
    /// Base controller for CRUD operations
    /// </summary>
    /// <typeparam name="TDto">Type of dto</typeparam>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public class CrudController<TDto, TEntity> : AppBaseController<CrudController<TDto, TEntity>>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly ICrudService<TDto, TEntity> _crudService;
        public CrudController(ICrudService<TDto, TEntity> crudService, IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// Creates resource from given model.
        /// </summary>
        /// <param name="createModel">Entity create model</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<int>> PostAsync([FromBody] TDto createModel)
        {
            var id = await _crudService.CreateAsync(createModel);
            return Ok(id);
        }
        
        /// <summary>
        /// Updates resource from given model.
        /// </summary>
        /// <param name="updateModel">Entity update model</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutAsync([FromBody] TDto updateModel)
        {
            await _crudService.UpdateAsync(updateModel);
            return Ok();
        }

        /// <summary>
        /// Deletes resource with given id.
        /// </summary>
        /// <param name="id">Id of the resource</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();   
        }
    }
}
