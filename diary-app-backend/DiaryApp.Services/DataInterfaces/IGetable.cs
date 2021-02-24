using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IGetable<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Returns all entities that match the given filter
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetByCriteriaAsync<TDto>(Expression<Func<TEntity, bool>> filter = null) where TDto : BaseDto;

        /// <summary>
        /// Returns first entity that match the given filter
        /// </summary>
        /// <returns></returns>
        Task<TDto> FirstOrDefaultAsync<TDto>(Expression<Func<TEntity, bool>> filter) where TDto : BaseDto;
    }
}
