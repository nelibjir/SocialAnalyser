using System.Net;

namespace SocialAnalyser.Exceptions
{
  public class BadRequestException: ApiException
  {
    public BadRequestException(string message)
      : base(HttpStatusCode.BadRequest, message) { }
  }
}
