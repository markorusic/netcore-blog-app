using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class RateRequestDto
    {
        [Required]
        [Range(1, 10)]
        public int Value { get; set; }
    }
}
