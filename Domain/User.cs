using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Rate> Rates { get; set; }

    }
}
