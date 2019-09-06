using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialAnalyser.Utils
{
  public static class FormFileExtensionUtil
  {
    /// <summary>
    /// Read the file and return string with new line marks 
    /// </summary>
    /// <param name="file">File to convert into string</param>
    /// <returns>File as string</returns>
    public static async Task<string> ReadAsListAsync(this IFormFile file)
    {
      StringBuilder result = new StringBuilder();
      using (StreamReader reader = new StreamReader(file.OpenReadStream()))
      {
        while (reader.Peek() >= 0)
          result.AppendLine(await reader.ReadLineAsync());
      }
      return result.ToString();
    }
  }
}
