using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data.Services
{
    public class CrudService<TDto, TEntity> : ICrudService<TDto, TEntity>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly IMapper mapper;
        protected internal ApplicationContext context;
        protected internal DbSet<TEntity> dbSet;

        public CrudService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<int> CreateAsync(TDto dto)
        {
            var entity = dto.ToEntity<TEntity, TDto>(mapper);
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async virtual Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await dbSet.AsNoTracking().ToListAsync();
            return entities.ToDtos<TEntity, TDto>(mapper);
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity.ToDto<TEntity, TDto>(mapper);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = dto.ToEntity<TEntity, TDto>(mapper);
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
