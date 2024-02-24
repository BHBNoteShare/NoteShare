using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteShare.Models;

namespace NoteShare.Core.Extensions
{
    public static class PaggingExtension
    {
        public static async Task<PagedResult<T>> GetPagedResult<T>(this IQueryable<T> values, int pageNum = 1, int pageSize = 10)
        {
            if (pageNum < 1)
            {
                pageNum = 1;
            }
            if (pageSize < 1 || pageSize > 20)
            {
                pageSize = 10;
            }
            var pagedResult = new PagedResult<T>()
            {
                ItemCount = await values.CountAsync(),
                PageNumber = pageNum,
                PageSize = pageSize,
                PageCount = (int)Math.Ceiling((double)await values.CountAsync() / pageSize),
                Items = await values.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync()
            };
            return pagedResult;
        }
    }
}
