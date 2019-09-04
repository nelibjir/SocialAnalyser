using MediatR;
using Microsoft.AspNetCore.Http;

namespace SocialAnalyser.Commands
{
  public class CreateDatasetCommand: IRequest
  {
    public IFormFile File { get; set; }

    public string Name { get; set; }
  }
}
