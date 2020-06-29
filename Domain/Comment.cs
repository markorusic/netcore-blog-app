using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment : BaseModel
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
