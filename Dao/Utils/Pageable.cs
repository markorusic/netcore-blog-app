using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Utils
{
    public abstract class Pageable
    {
        public int Page { get; set; }

        public int Size { get; set; }
    }
}
