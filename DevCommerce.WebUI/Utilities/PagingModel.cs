using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.WebUI.Utilities
{
    public class PagingModel<T> where T : class
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get { return Convert.ToInt32(Math.Ceiling((decimal)(_pageData.Count / PageSize))); } }

        private IList<T> _pageData;
        public IList<T> PagedData
        {
            get { return _pageData.Skip(PageSize * CurrentPage).Take(PageSize).ToList(); }
            set { _pageData = value; }
        }
    }
}
