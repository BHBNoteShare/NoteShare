using AutoMapper;
using NoteShare.Models;

namespace NoteShare.Core.MapperConfigs
{
    public class DefaultMapperConfig : Profile
    {
        public DefaultMapperConfig()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>)).ConvertUsing(typeof(PagedResultConverter<,>));
        }
    }

    public class PagedResultConverter<TSource, TDestination> : ITypeConverter<PagedResult<TSource>, PagedResult<TDestination>>
    {
        public PagedResult<TDestination> Convert(PagedResult<TSource> source, PagedResult<TDestination> destination, ResolutionContext context)
        {
            return new PagedResult<TDestination>
            {
                Items = context.Mapper.Map<List<TSource>, List<TDestination>>(source.Items),
                ItemCount = source.ItemCount,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize
            };
        }
    }
}
