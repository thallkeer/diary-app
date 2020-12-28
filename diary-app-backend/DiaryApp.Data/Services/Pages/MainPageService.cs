using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class MainPageService : PageService<MainPageDto, MainPage>, IMainPageService
    {
        public MainPageService(ApplicationContext context, IMapper mapper, IUserService userService) : base(context, mapper, userService)
        {
        }

        public async Task<IEnumerable<MainPageDto>> GetAsync(Expression<Func<MainPage, bool>> filter = null)
        {
            IQueryable<MainPage> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var result = await query.ToListAsync();

            return result.ToDtos<MainPage, MainPageDto>(mapper);
        }
    }
}
