using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dto.Request
{
    public class PostRequestDto
    {
        [Required(ErrorMessage = "Required field")]
        [MinLength(5, ErrorMessage = "Must be at least 5 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MinLength(15, ErrorMessage = "Must be at least 15 characters long")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MinLength(20, ErrorMessage = "Must be at least 20 characters long")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)", ErrorMessage = "Invalid image URL format")]
        public string MainPhoto { get; set; }

        public IEnumerable<string> Photos { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<int> Categories { get; set; } = Enumerable.Empty<int>();
    }
}
