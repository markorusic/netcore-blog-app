using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Utils
{
    public class Page<T> : PagedResultBase where T : class
    {
        public IList<T> Content { get; set; }

        public Page()
        {
            Content = new List<T>();
        }
    }
}
