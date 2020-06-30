using System.Net;

namespace Common
{
    public class ResourceNotFoundException : HttpException
    {
        public ResourceNotFoundException(string resourceName) : base(HttpStatusCode.NotFound, $"{resourceName} not found!")
        {
        }
    }
}
