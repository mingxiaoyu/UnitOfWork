using System.Collections.Generic;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public static class IEnumerableExtensions
    {
        public static IPagedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageIndex = 1, int pageSize = 10) where T : class
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }
    }
}