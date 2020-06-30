using System.Net;

namespace ResourceException
{
    public class ForbiddenActionException : HttpException
    {
        public ForbiddenActionException() : base(HttpStatusCode.Forbidden, "Forbidden")
        {
        }
    }
}
