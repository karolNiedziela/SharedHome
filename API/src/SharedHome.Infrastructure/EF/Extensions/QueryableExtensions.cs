using Microsoft.EntityFrameworkCore;
using SharedHome.Shared.Abstractions.Queries;

namespace SharedHome.Infrastructure.EF.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<Paged<T>> PaginateAsync<T>(this IQueryable<T> data, int page, int items)
        {
            var totalItems = await data.CountAsync();
            var totalPages = totalItems <= items ? 1 : (int)Math.Floor((double)totalItems / items);
            var result = await data.Skip((page - 1) * items).Take(items).ToListAsync();

            return new Paged<T>(result, page, items, totalPages, totalItems);
        }
    }
}
