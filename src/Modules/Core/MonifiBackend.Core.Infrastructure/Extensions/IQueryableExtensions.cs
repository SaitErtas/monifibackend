﻿using MonifiBackend.Core.Domain.Resources;
using System.Linq.Expressions;

namespace MonifiBackend.Core.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryObject queryObj)
        {
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10;

            if (queryObj.Page <= 0)
                queryObj.Page = 1;

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }

        public static IQueryable<T> ApplyFiltering<T, M>(this IQueryable<T> query, M filter, Dictionary<bool, Expression<Func<T, bool>>> columns)
        {
            foreach (var column in columns)
            {
                var expression = column.Value;
                var hasValue = column.Key;

                if (hasValue)
                {
                    query = query.Where(expression);
                }
            }
            return query;
        }
    }
}
