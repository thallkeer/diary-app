using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Data.Services
{
    public class CrudService<TDto, TEntity> : ICrudService<TDto, TEntity>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected internal ApplicationContext _context;
        protected internal DbSet<TEntity> _dbSet;

        public CrudService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<int> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new EntityNotFoundException($"Entity of type {typeof(TEntity)} with such id is not found!");
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByCriteriaAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = _dbSet.AsNoTracking();

            if (filter != null)
                entities = entities.Where(filter);

            return await entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(en => en.Id == id);
            return entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns queryable db set of non tracking entities
        /// </summary>
        /// <returns></returns>
        protected IQueryable<TEntity> GetAsQueryable()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
