using DiaryApp.Core.Entities;
using DiaryApp.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiaryApp.Services.DataInterfaces
{
    public interface IGetable<TDto, TEntity>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Returns all entities that match the given filter
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TCustomDto>> GetByCriteriaAsync<TCustomDto>(Expression<Func<TEntity, bool>> filter = null) where TCustomDto : TDto;

        /// <summary>
        /// Returns first entity that match the given filter
        /// </summary>
        /// <returns></returns>
        Task<TCustomDto> FirstOrDefaultAsync<TCustomDto>(Expression<Func<TEntity, bool>> filter) where TCustomDto : TDto;
    }
}
