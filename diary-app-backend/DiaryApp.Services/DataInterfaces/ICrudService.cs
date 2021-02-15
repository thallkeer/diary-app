using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface ICrudService<TDto, TEntity> : IGetable<TEntity>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        /// <summary>
        /// Saves the entity from given dto to datasource and returns it's identifier
        /// </summary>
        /// <param name="entity">Dto</param>
        /// <returns></returns>
        Task<int> CreateAsync(TDto entity);        

        /// <summary>
        /// Returns entity by given id or null if it's not exists
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        Task<TDto> GetByIdAsync(int id);

        /// <summary>
        /// Updates the given entity
        /// </summary>
        /// <param name="entity">Dto with changes to update</param>
        /// <returns></returns>
        Task UpdateAsync(TDto entity);

        /// <summary>
        /// Delete entity with given id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
