using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models.PageAreas;
using DiaryApp.Data.Extensions;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    public class PageAreaService<TAreaDto, TArea, TPage> : IPageAreaService<TAreaDto, TArea, TPage>
        where TAreaDto : PageAreaDto
        where TArea : PageAreaBase<TPage>
        where TPage : PageBase
    {
        protected readonly IMapper mapper;
        protected internal ApplicationContext context;
        protected internal DbSet<TArea> dbSet;

        public PageAreaService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = context.Set<TArea>();
        }

        public async Task<TAreaDto> GetPageAreaByPage(int pageID)
        {
            var entity = await dbSet.FirstOrDefaultAsync(area => area.Page.Id == pageID);
            return entity.ToDto<TArea, TAreaDto>(mapper);
        }
    }
}
