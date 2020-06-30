using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class AuthRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}