using System.Net;

namespace ResourceException
{
    public class ResourceNotFoundException : HttpException
    {
        public ResourceNotFoundException(string resourceName) : base(HttpStatusCode.NotFound, $"{resourceName} not found!")
        {
        }
    }
}
