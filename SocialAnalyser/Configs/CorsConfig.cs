using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SocialAnalyser.Configs
{
  public class CorsConfig
  {
    public static void SetupCors(CorsPolicyBuilder cors)
    {
      cors.AllowAnyHeader();
      cors.AllowAnyMethod();
      cors.AllowAnyOrigin();
    }
  }
}
