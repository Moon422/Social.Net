using Microsoft.EntityFrameworkCore;
using Social.Net.Core;
using Social.Net.Core.Domains.Common;

namespace Social.Net.Data;

public static class QueryableExtensions
{
    public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> query, 
        int pageIndex = 0, int pageSize = int.MaxValue) where TEntity : BaseEntity
    {
        var data = await query.Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<TEntity>
        {
            Data = data,
            PageIndex = pageIndex,
            TotalCount = await query.CountAsync(),
            MaximumPageItemCount = pageSize
        };
    }
}