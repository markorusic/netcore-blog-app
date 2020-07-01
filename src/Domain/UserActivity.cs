using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UserActivity : BaseModel
    {
        public string ActionType { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
