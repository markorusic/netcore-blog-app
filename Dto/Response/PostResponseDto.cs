using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.Response
{
    public class PostResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string MainPhoto { get; set; }

        public int UserId { get; set; }

        //public UserResponseDto User { get; set; }

        //public ICollection<Photo> Photos { get; set; }

        //public ICollection<PostCategory> Categories { get; set; }
    }
}
