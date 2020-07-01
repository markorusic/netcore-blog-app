using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Service.Impl
{
    public class FileUploadServiceImpl : IFileUploadService {

        private readonly IWebHostEnvironment _env;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IUserActivity _userActivityService;

        public FileUploadServiceImpl(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IUserActivity userActivityService)
        {
            _env = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _userActivityService = userActivityService;
        }
        
        public string UploadImage(IFormFile file)
        {
            try
            {
                var extension = Path.GetExtension(file.FileName);
                var allowedExtensions = new List<string>() { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Any(e => e == extension))
                {
                    throw new HttpException(HttpStatusCode.BadRequest, "image extension should be on of the following: jpg, jpeg, png");
                }

                var rootPath = _env.WebRootPath;
                var fileName = "image_" + DateTime.Now.ToFileTime().ToString() + extension;
                var dirName = "Images";

                var dirPath = Path.Combine(rootPath, $"{dirName}");
                Directory.CreateDirectory(dirPath);

                var imagePath = $"{dirPath}/{fileName}";
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
                var fullPath = $"{baseUrl}/{dirName}/{fileName}";

                _userActivityService.Track($"Uploaded photo: {fullPath}");

                return fullPath;
            }
            catch (Exception ex)
            {
                throw new HttpException(HttpStatusCode.UnprocessableEntity, ex.Message);
            }
        }
    }
}
