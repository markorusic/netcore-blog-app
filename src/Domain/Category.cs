using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }
    }
}
