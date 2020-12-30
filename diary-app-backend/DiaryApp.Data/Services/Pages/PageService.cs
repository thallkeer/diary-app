using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

        public async Task<TPageEntity> GetPageAsync(int userID, int year, int month)
        {
            return await GetOneByCriteriaOrDefaultAsync(mp => mp.UserId == userID && mp.Month == month && mp.Year == year);
        }

        public async Task<TPageDto> CreateAsync(PageDto pageDto, bool initializePageAreas)
        {
            var createdPage = await CreateAsync(pageDto.UserId, pageDto.Year, pageDto.Month, initializePageAreas);
            return _mapper.Map<TPageDto>(createdPage);
        }

        public async Task<TPageDto> CreateAsync(int userID, int year, int month)
        {
            var createdPage = await CreateAsync(userID, year, month, true);
            return _mapper.Map<TPageDto>(createdPage);
        }

        protected async Task<TPageEntity> CreateAsync(int userID, int year, int month, bool initializePageAreas)
        {
            //var pageExists = await _dbSet.AnyAsync(mp => mp.UserId == userID && mp.Month == month && mp.Year == year);
            //if (pageExists)
            //    throw new PageAlreadyExistsException("Page with such parameters already exists");

            if (month <= 0 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            if (year < 2020)
                throw new ArgumentOutOfRangeException(nameof(year));

            //if (!await userService.IsUserExists(userID))
            //    throw new UserNotExistsException("User with such id is not found");

            var page = new TPageEntity()
            {
                UserId = userID,
                Year = year,
                Month = month
            };

            if (initializePageAreas)
                page.CreateAreas();

            await _dbSet.AddAsync(page);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(page.Id);
        }

        public async Task<TPageArea> GetPageArea<TPageArea>(int pageID) where TPageArea : class, IPageArea
        {
            var entity = await _context.Set<TPageArea>().FirstOrDefaultAsync(area => area.PageId == pageID);
            return entity;
        }
    }
}
