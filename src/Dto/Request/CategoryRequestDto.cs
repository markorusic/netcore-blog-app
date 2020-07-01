using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
