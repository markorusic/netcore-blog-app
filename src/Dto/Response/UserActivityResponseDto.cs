using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Response
{
    public class UserActivityResponseDto
    {
        public int Id { get; set; }

        public string ActionType { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
