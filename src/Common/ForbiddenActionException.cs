using System.Net;

namespace Common
{
    public class ForbiddenActionException : HttpException
    {
        public ForbiddenActionException() : base(HttpStatusCode.Forbidden, "Forbidden")
        {
        }
    }
}
