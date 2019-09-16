namespace SocialAnalyser
{
  public class ApiError
  {
    public ApiError(string message, int statusCode)
    {
      Message = message;
      StatusCode = statusCode;
    }

    public string Message { get; }

    public int StatusCode { get; }
  }
}
