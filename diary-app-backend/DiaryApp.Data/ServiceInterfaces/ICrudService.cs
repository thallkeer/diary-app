using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface ICrudService<TDto, TEntity> : ICreatable<TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto> GetByIdAsync(int id);

        Task UpdateAsync(TDto entity);

        Task DeleteAsync(int id);
    }
}
