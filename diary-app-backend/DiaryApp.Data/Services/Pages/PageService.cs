using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Interfaces;
using DiaryApp.Data.Exceptions;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public abstract class PageService<TPageDto, TPageEntity> : IPageService<TPageDto>
        where TPageDto : PageDto
        where TPageEntity : PageBase, new()
    {
        protected readonly IUserService userService;
        protected readonly IMapper mapper;
        protected readonly ApplicationContext context;
        protected readonly DbSet<TPageEntity> dbSet;

        public PageService(ApplicationContext context, IMapper mapper, IUserService userService)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TPageEntity>();
            this.userService = userService;
        }

        public async Task<TPageDto> GetPageAsync(int userID, int year, int month)
        {
            var page = await dbSet.FirstOrDefaultAsync
               (mp => mp.UserID == userID && mp.Month == month && mp.Year == year);

            return page.ToDto<TPageEntity, TPageDto>(mapper);
        }

        public async Task<TPageDto> CreateAsync(PageDto pageDto, bool initializePageAreas)
        {
            return await CreateAsync(pageDto.UserID, pageDto.Year, pageDto.Month, initializePageAreas);
        }

        public async Task<TPageDto> CreateAsync(int userID, int year, int month)
        {
            return await CreateAsync(userID, year, month, true);
        }

        private async Task<TPageDto> CreateAsync(int userID, int year, int month, bool initializePageAreas)
        {
            var pageExists = await dbSet.AnyAsync(mp => mp.UserID == userID && mp.Month == month && mp.Year == year);
            if (pageExists)
                throw new PageAlreadyExistsException("Page with such parameters already exists");

            if (month <= 0 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            if (year < 2020)
                throw new ArgumentOutOfRangeException(nameof(year));

            if (!await userService.IsUserExists(userID))
                throw new UserNotExistsException("User with such id is not found");

            var page = new TPageEntity()
            {
                UserID = userID,
                Year = year,
                Month = month
            };

            if (initializePageAreas)
                page.CreateAreas();

            await dbSet.AddAsync(page);

            await context.SaveChangesAsync();

            return page.ToDto<TPageEntity, TPageDto>(mapper);
        }

        public async Task<TPageAreaDto> GetPageArea<TPageAreaDto, TPageArea>(int pageID)
            where TPageAreaDto : PageAreaDto
            where TPageArea : class, IPageArea
        {
            var entity = await context.Set<TPageArea>().FirstOrDefaultAsync(area => area.PageId == pageID);
            return entity.ToDto<TPageArea, TPageAreaDto>(mapper);
        }        
    }
}
