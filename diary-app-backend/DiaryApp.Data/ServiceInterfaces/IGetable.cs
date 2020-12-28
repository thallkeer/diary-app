using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface IGetable<TDto, TEntity>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        /// <summary>
        /// Returns entities, that match the given criteria
        /// </summary>
        /// <param name="filter">Criteria</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
