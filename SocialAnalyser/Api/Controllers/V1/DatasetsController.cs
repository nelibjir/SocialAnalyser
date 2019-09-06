using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Commands;
using SocialAnalyser.Exceptions;

namespace SocialAnalyser.Api.Controllers.V1
{
  [Route("api/v1/datasets")]
  public class DatasetsController: Controller
  {

    private readonly IMediator fMediator;
    private static readonly log4net.ILog fLog = log4net.LogManager.GetLogger(typeof(DatasetsController));

    public DatasetsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Get statistics for the named dataset
    /// </summary>
    /// <param name="name">Name of dataset for the</param>
    /// <returns>Average number of friends and number of users in that dataset</returns>
    [HttpGet, Route("{name}", Name = nameof(GetDataset))]
    public async Task<DatasetStatistics> GetDataset(string name, CancellationToken cancellationToken)
    {
      if (string.IsNullOrEmpty(name))
        throw new BadRequestException($"Please specify the name of the dataset");

      return await fMediator.Send(new GetDatasetStatisticsCommand { Name = name }, cancellationToken);
    }

    /// <summary>
    /// Creates new dataset in the database with users and their friends from
    /// a file sent in request as binary in data form with its name
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>if successful returns name of the added dataset and code 201</returns>
    [HttpPost]
    public async Task<ActionResult> PostNewDataset(CancellationToken cancellationToken)
    {
      if(Request.Form == null || Request.Form.Files == null || Request.Form.Files.Count == 0)
        throw new BadRequestException($"File has not been sent");
      if(Request.Form.Files.Count > 1)
        throw new BadRequestException($"Only one file can be send at once");

      bool hasName = Request.Form.TryGetValue(NewDataset.NameKey, out StringValues datasetName);
      if (!hasName)
        throw new BadRequestException($"No name of the file in the request");

      await fMediator.Send(new CreateDatasetCommand { File = Request.Form.Files[0], Name = datasetName }, cancellationToken);
      return CreatedAtRoute(nameof(GetDataset), new { name = datasetName }, datasetName);
    }
  }
}
