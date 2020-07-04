using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.Core
{
    public interface ICrudService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);
    }
}
