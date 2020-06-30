using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class AuthRequestDto
    {
        [Required(ErrorMessage = "Required field")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string Password { get; set; }

    }
}