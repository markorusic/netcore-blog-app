using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Response
{
    public class RateResponseDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
