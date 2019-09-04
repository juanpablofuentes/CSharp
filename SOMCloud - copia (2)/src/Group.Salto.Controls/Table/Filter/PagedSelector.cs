using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Controls.Table.Filter
{
    public class PagedSelector<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; private set; }

        public IEnumerable<T> GetPage(IList<T> source)
        {
            if (PageSize == 0)
            {
                PagesCount = 1;
                return source.ToList();
            }
            PagesCount = (int)Math.Ceiling((double)source.Count() / (double)PageSize);
            return source.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
        }

        public IEnumerable<T> GetPageIEnumerable(IEnumerable<T> source)
        {
            if (PageSize == 0)
            {
                PagesCount = 1;
                return source.ToList();
            }
            PagesCount = (int)Math.Ceiling((double)source.Count() / (double)PageSize);
            Page = Math.Min(PagesCount, Page);
            return source.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
        }
    }

}
