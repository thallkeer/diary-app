using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;
using DiaryApp.Services.DataInterfaces.Pages;
using DiaryApp.Services.DataInterfaces.Users;
using DiaryApp.Services.DTO;
using DiaryApp.Services.DTO.PageAreas;
using DiaryApp.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DiaryApp.Services.DataServices.Pages
{
    public abstract class PageService<TPageDto, TPageEntity> : CrudService<TPageDto, TPageEntity>, IPageService<TPageDto, TPageEntity>
        where TPageDto : PageDto
        where TPageEntity : PageBase, new()
    {
        protected readonly IUserService UserService;

        public PageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper)
        {
            UserService = userService;
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

        public async Task<TPageAreaDto> GetPageAreaOrThrowAsync<TPageArea, TPageAreaDto>(int pageId) 
            where TPageArea : PageAreaBase<TPageEntity>
            where TPageAreaDto : PageAreaDto
        {
            var entity = await _context.Set<TPageArea>().FirstOrDefaultAsync(area => area.PageId == pageId);
            if (entity == null)
                throw new EntityNotFoundException<TPageArea>();
            return _mapper.Map<TPageAreaDto>(entity);
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
