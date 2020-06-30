using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _uploadService;

        public FileUploadController(IFileUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("image")]
        public IActionResult ImageUpload([FromForm] ImageUploadRequest request)
        {
            var path = _uploadService.UploadImage(request.Image);
            return Ok(new { path });
        }

    }
}
