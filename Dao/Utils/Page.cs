using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Utils
{
    public class Page<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public Page()
        {
            Results = new List<T>();
        }
    }
}
