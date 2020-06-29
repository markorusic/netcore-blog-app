using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PostPhoto : BaseModel
    {
        public string Src { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
