using System.Linq;
using System.Threading.Tasks;
using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data.Services
{
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : class
    {
        protected internal ApplicationContext context;
        protected internal DbSet<TEntity> dbSet;

        public CrudService(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Update(TEntity entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
