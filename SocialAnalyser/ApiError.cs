using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
