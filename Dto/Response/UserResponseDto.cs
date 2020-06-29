using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Username{ get; set; }

        public string Email { get; set; }

        //public IEnumerable<PostResponseDto> Posts { get; set; }

        //public ICollection<Comment> Comments { get; set; }

        //public ICollection<Rate> Rates { get; set; }

    }
}
