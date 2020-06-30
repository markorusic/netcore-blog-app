using Microsoft.AspNetCore.Http;

namespace Service
{
    public interface IFileUploadService
    {
        public string UploadImage(IFormFile file);
    }
}
