using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Rate : BaseModel
    {
        public int Value { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
