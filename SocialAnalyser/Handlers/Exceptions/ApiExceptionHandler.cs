using System;
using SocialAnalyser.Exceptions;

namespace SocialAnalyser.Handlers.Exceptions
{
  public class ApiExceptionHandler: IExceptionHandler
  {
    public bool CanHandle(Exception ex)
    {
      return ex is ApiException;
    }

    public ApiError Handle(Exception ex)
    {
      ApiException apiEx = ex as ApiException;
      return new ApiError(apiEx.Message, (int)apiEx.StatusCode);
    }
  }
}
