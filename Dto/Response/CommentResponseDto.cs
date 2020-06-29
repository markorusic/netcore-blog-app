using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Response
{
    public class CommentResponseDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }
    }
}
