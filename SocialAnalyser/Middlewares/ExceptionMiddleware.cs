using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SocialAnalyser.Factories;

namespace SocialAnalyser.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly RequestDelegate fNext;
    private readonly ApiErrorFactory fApiErrorFactory;

    public ExceptionMiddleware(RequestDelegate next, ApiErrorFactory apiErrorFactory)
    {
      fNext = next;
      fApiErrorFactory = apiErrorFactory;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await fNext(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      ApiError apiError = fApiErrorFactory.Create(exception);
      context.Response.StatusCode = apiError.StatusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = apiError.Message }));
    }
  }
}
