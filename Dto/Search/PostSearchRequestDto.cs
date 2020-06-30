using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class PostSearchRequestDto : PageRequest
    {
        public string Title { get; set; }

        public int? CategoryId { get; set; }
    }
}
