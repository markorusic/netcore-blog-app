using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dao.Utils
{
    public static class PageExtension
    {
        public static Page<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new Page<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
 
            var skip = (page - 1) * pageSize;     
            result.Content = query.Skip(skip).Take(pageSize).ToList();
 
            return result;
        }
    }
}
