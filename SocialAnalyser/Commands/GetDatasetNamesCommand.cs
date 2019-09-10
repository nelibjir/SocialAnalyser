using MediatR;
using SocialAnalyser.Api.Models;

namespace SocialAnalyser.Commands
{
  public class GetDatasetNamesCommand: IRequest<DatasetNames>
  {
  }
}
