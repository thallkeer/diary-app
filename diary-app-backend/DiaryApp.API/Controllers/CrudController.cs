using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace DiaryApp.API.Controllers
{
    /// <summary>
    /// Base controller for CRUD operations
    /// </summary>
    /// <typeparam name="TDto">Type of dto</typeparam>
    /// <typeparam name="TEntity">Type of entity</typeparam>
    public class CrudController<TDto, TEntity> : DiaryAppController
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly ICrudService<TDto, TEntity> _crudService;
        public CrudController(ICrudService<TDto, TEntity> crudService)
        {
            _crudService = crudService;
        }

        /// <summary>
        /// Returns resource with given id if exists.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<TDto>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _crudService.GetByIdAsync(id);
            return entity == null ? (ActionResult<TDto>) NotFound() : Ok(Mapper.Map<TDto>(entity));
        }

        /// <summary>
        /// Creates resource from given model.
        /// </summary>
        /// <param name="createModel">Entity create model</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<int>> PostAsync([FromBody] TDto createModel, CancellationToken cancellationToken = default)
        {
            var id = await _crudService.CreateAsync(createModel);
            return Ok(id);
        }

        /// <summary>
        /// Updates resource from given model.
        /// </summary>
        /// <param name="updateModel">Entity update model</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutAsync([FromBody] TDto updateModel, CancellationToken cancellationToken = default)
        {
            await _crudService.UpdateAsync(updateModel);
            return Ok();
        }

        /// <summary>
        /// Deletes resource with given id.
        /// </summary>
        /// <param name="id">Id of the resource</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _crudService.DeleteAsync(id);
            return NoContent();
        }
    }
}
