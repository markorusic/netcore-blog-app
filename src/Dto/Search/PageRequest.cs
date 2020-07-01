using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PageRequest
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;
    }
}
