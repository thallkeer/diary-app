using AutoMapper;
using DiaryApp.Core.DTO;
using System.Collections.Generic;

namespace DiaryApp.Data.Extensions
{
    public static class ServiceExtensions
    {
        public static IEnumerable<TDto> ToDtos<TEntity, TDto>(this IEnumerable<TEntity> entities, IMapper mapper) 
            where TEntity : class
            where TDto : BaseDto
        {
            return mapper.Map<IEnumerable<TDto>>(entities);
        }

        public static TDto ToDto<TEntity, TDto>(this TEntity entity, IMapper mapper)
            where TEntity : class
            where TDto : BaseDto
        {
            return mapper.Map<TDto>(entity);
        }

        public static TEntity ToEntity<TEntity, TDto>(this TDto dto, IMapper mapper)
           where TEntity : class
           where TDto : BaseDto
        {
            return mapper.Map<TEntity>(dto);
        }
    }
}
