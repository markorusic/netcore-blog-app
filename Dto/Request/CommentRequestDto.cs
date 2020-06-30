using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class CommentRequestDto
    {
        [Required(ErrorMessage = "Required field")]
        public string Content { get; set; }
    }
}