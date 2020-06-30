using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dto.Request
{
    public class PostRequestDto
    {
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(15)]
        public string Description { get; set; }

        [Required]
        [MinLength(20)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", ErrorMessage = "Invalid image URL format")]
        public string MainPhoto { get; set; }

        public IEnumerable<string> Photos { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<int> Categories { get; set; } = Enumerable.Empty<int>();
    }
}
