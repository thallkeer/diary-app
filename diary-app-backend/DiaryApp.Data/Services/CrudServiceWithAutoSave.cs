using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;
using System.Threading.Tasks;

namespace DiaryApp.Data.Services
{
    /// <summary>
    /// Service, that provides autosave for each non-read operation
    /// </summary>
    /// <typeparam name="TDto">Dto type</typeparam>
    /// <typeparam name="TEntity">Domain model type</typeparam>
    public class CrudServiceWithAutoSave<TDto, TEntity> : CrudService<TDto, TEntity>, ICrudService<TDto, TEntity>
        where TDto : BaseDto
        where TEntity : BaseEntity
    {
        public CrudServiceWithAutoSave(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async override Task<int> CreateAsync(TDto dto)
        {
            var id = await base.CreateAsync(dto);
            await context.SaveChangesAsync();
            return id;
        }

        public async override Task UpdateAsync(TDto dto)
        {
            await base.UpdateAsync(dto);
            await context.SaveChangesAsync();
        }

        public async override Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
            await context.SaveChangesAsync();
        }
    }
}
