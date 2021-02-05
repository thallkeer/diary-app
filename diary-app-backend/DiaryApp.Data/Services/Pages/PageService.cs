using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.DataInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DiaryApp.Data.Exceptions;
using DiaryApp.Core.Entities;

namespace DiaryApp.Data.Services
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

        protected async Task<TPageEntity> GetPageEntityAsync(int userID, int year, int month)
        {
            return await FirstOrDefaultAsync(mp => mp.UserId == userID && mp.Month == month && mp.Year == year);
        }

        public async Task<TPageDto> GetPageAsync(int userID, int year, int month)
        {
            var page = await GetPageEntityAsync(userID, year, month);
            return _mapper.Map<TPageDto>(page);
        }
        public virtual async Task<TPageDto> CreateAsync(int userID, int year, int month)
        {
            var pageExists = await _dbSet.AnyAsync(mp => mp.UserId == userID && mp.Month == month && mp.Year == year);
            if (pageExists)
                throw new PageAlreadyExistsException("Page with such parameters already exists");

            var page = new TPageEntity()
            {
                UserId = userID,
                Year = year,
                Month = month
            };

            page.CreateAreas();

            await _dbSet.AddAsync(page);
            await _context.SaveChangesAsync();

            var createdPage = await GetByIdAsync(page.Id);
            return _mapper.Map<TPageDto>(createdPage);
        }

        public async Task<TPageArea> GetPageArea<TPageArea>(int pageID) where TPageArea : class, IPageArea
        {
            var entity = await _context.Set<TPageArea>().FirstOrDefaultAsync(area => area.PageId == pageID);
            return entity;
        }
    }
}
