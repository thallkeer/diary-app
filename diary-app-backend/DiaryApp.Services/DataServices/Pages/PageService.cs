using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Services.DataInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DiaryApp.Services.Exceptions;
using DiaryApp.Core.Entities;
using System;
using System.Linq.Expressions;

namespace DiaryApp.Services.DataServices
{
    public abstract class PageService<TPageDto, TPageEntity> : CrudService<TPageDto, TPageEntity>, IPageService<TPageDto, TPageEntity>
        where TPageDto : PageDto
        where TPageEntity : PageBase, new()
    {
        protected readonly IUserService userService;

        public PageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper)
        {
            this.userService = userService;
        }

        public async Task<TPageDto> GetPageAsync(int userId, int year, int month)
        {
            return await FirstOrDefaultAsync<TPageDto>(BuildGetPageExpression(userId, year, month));
        }

        public virtual async Task<TPageDto> CreateAsync(int userId, int year, int month)
        {
            var pageExists = await _dbSet.AnyAsync(BuildGetPageExpression(userId, year, month));
            if (pageExists)
                throw new PageAlreadyExistsException();

            var page = new TPageEntity()
            {
                UserId = userId,
                Year = year,
                Month = month
            };

            page.CreateAreas();

            await _dbSet.AddAsync(page);
            await _context.SaveChangesAsync();

            var createdPage = await GetByIdAsync(page.Id);
            return _mapper.Map<TPageDto>(createdPage);
        }

        public async Task<TPageArea> GetPageAreaOrThrowAsync<TPageArea>(int pageID) where TPageArea : class, IPageArea
        {
            var entity = await _context.Set<TPageArea>().FirstOrDefaultAsync(area => area.PageId == pageID);
            if (entity == null)
                throw new EntityNotFoundException<TPageArea>();
            return entity;
        }

        protected Expression<Func<TPageEntity, bool>> BuildGetPageExpression(int userId, int year, int month)
        {
            return mp => mp.UserId == userId && mp.Month == month && mp.Year == year;
        }

        protected async Task<TPageEntity> GetPageEntityAsync(int userId, int year, int month)
        {
            return await _dbSet.FirstOrDefaultAsync(BuildGetPageExpression(userId, year, month));
        }
    }
}
