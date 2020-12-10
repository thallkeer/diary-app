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
        protected readonly IMapper mapper;
        protected readonly ApplicationContext context;
        protected readonly DbSet<TPageEntity> dbSet;
        public PageService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TPageEntity>();
        }

        public virtual async Task<TPageDto> GetPageForUser(int userID, int year, int month)
        {
            var page = await dbSet.FirstOrDefaultAsync
               (mp => mp.UserID == userID && mp.Month == month && mp.Year == year);

            return page.ToDto<TPageEntity, TPageDto>(mapper);
        }

        public virtual async Task<TPageDto> CreatePageByParams(int userID, int year, int month)
        {
            var existingPage = await GetPageForUser(userID, year, month);
            if (existingPage != null)
                throw new PageAlreadyExistsException();

            if (month <= 0 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            if (year < 2020)
                throw new ArgumentOutOfRangeException(nameof(year));

            var page = new TPageEntity()
            {
                UserID = userID,
                Year = year,
                Month = month
            };

            page.CreateAreas();

            await dbSet.AddAsync(page);

            //try
            //{
                await context.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{

            //}

            var dto = page.ToDto<TPageEntity, TPageDto>(mapper);

            return dto;
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
