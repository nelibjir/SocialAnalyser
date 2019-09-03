using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialAnalyser.Utils
{
  public static class FormFileExtensionUtil
  {
    public static async Task<string> ReadAsListAsync(this IFormFile file)
    {
      StringBuilder result = new StringBuilder();
      using (var reader = new StreamReader(file.OpenReadStream()))
      {
        while (reader.Peek() >= 0)
          result.AppendLine(await reader.ReadLineAsync());
      }
      return result.ToString();
    }
  }
}
