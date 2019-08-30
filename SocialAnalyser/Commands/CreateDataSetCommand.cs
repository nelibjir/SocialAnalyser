using MediatR;

namespace SocialAnalyser.Commands
{
  public class CreateDataSetCommand: IRequest
  {
    public string Dataset { get; set; }
  }
}
