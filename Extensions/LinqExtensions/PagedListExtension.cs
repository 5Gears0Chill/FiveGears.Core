using System;
using System.Collections.Generic;
using System.Linq;

namespace FiveGears.Core.Extensions.LinqExtensions
{
    public static class PagedListExtension
    {
        public static IQueryable<T> PageQuerable<T>(this IQueryable<T> query, int page, int pageSize)
        {
            int skip = Math.Max(pageSize * (page - 1), 0);
            return query.Skip(skip).Take(pageSize);
        }

        public static IEnumerable<T> PageEnumerable<T>(this IEnumerable<T> query, int page, int pageSize)
        {
            int skip = Math.Max(pageSize * (page - 1), 0);
            return query.Skip(skip).Take(pageSize);
        }
    }
}
