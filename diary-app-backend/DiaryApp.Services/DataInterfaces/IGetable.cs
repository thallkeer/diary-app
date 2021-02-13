using DiaryApp.Core.Entities;
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
        Task<IEnumerable<TEntity>> GetByCriteriaAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// Returns first entity that match the given filter
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
    }
}
