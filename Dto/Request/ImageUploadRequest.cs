using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dto.Request
{
    public class ImageUploadRequest
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
