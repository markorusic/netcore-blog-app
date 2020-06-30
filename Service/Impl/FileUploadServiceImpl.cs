using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ResourceException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Service.Impl
{
    public class FileUploadServiceImpl : IFileUploadService {

        private readonly IWebHostEnvironment _env;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileUploadServiceImpl(IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _env = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string UploadImage(IFormFile file)
        {
            try
            {
                string rootPath = _env.WebRootPath;
                var dirName = "Images";

                var dirPath = Path.Combine(rootPath, $"{dirName}");
                Directory.CreateDirectory(dirPath);

                var imagePath = $"{dirPath}/{file.FileName}";
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
                return $"{baseUrl}/{dirName}/{file.FileName}";
            }
            catch (Exception ex)
            {
                throw new HttpException(HttpStatusCode.UnprocessableEntity, ex.Message);
            }
        }
    }
}
