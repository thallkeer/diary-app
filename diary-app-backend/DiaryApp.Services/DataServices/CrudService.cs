﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.Exceptions;
using DiaryApp.Services.DataInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Services.DataServices
{
    public class CrudService<TDto, TEntity> : ICrudService<TDto, TEntity>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public CrudService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
                throw new EntityNotFoundException<TEntity>();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }        

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(en => en.Id == id);
            return _mapper.Map<TDto>(entity);
        }        

        public virtual async Task UpdateAsync(TDto dto)
        {
            var entity = await _dbSet.FindAsync(dto.Id);
            var mappedEntity = _mapper.Map(dto, entity);
            _context.Update(mappedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TCustomDto>> GetByCriteriaAsync<TCustomDto>(Expression<Func<TEntity, bool>> filter = null)
            where TCustomDto : TDto
        {
            var entities = GetAsQueryable();

            if (filter != null)
                entities = entities.Where(filter);

            return await entities.Select(en => _mapper.Map<TCustomDto>(en)).ToListAsync();
        }

        public async Task<TCustomDto> FirstOrDefaultAsync<TCustomDto>(Expression<Func<TEntity, bool>> filter)
            where TCustomDto : TDto
        {
            var entity = await _dbSet.FirstOrDefaultAsync(filter);
            return _mapper.Map<TCustomDto>(entity);
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
