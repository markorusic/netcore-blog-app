using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Search
{
    public class UserActivitySearchDto : PageRequest
    {
        public string ActionType { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
