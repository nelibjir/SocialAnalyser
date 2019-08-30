using System;

namespace SocialAnalyser.Handlers.Exceptions
{
  public interface IExceptionHandler
  {
    bool CanHandle(Exception ex);

    ApiError Handle(Exception ex);
  }
}
