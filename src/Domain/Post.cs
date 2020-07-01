using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Post : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string MainPhoto { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public ICollection<PostCategory> Categories { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rate> Rates { get; set; }
    }
}
