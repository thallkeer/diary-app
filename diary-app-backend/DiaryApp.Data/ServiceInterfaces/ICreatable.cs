using DiaryApp.Core.DTO;
using System.Threading.Tasks;

namespace DiaryApp.Data.ServiceInterfaces
{
    public interface ICreatable<TDto> where TDto : BaseDto
    {
        Task<int> CreateAsync(TDto entity);
    }
}
