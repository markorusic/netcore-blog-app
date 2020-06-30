using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class CommentRequestDto
    {
        [Required]
        public string Content { get; set; }
    }
}