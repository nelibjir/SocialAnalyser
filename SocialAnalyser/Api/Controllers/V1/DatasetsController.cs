using System.Net;
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

    public DatasetsController(IMediator mediator)
    {
      fMediator = mediator;
    }

    /// <summary>
    /// Creates new dataset in the database with users and their friends from
    /// a file sent in request as binary in data form with its name
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation</param>
    /// <returns>if successful returns name of the added dataset and code 201</returns>
    /// <response code="400">The request is not correct, excpected FileForm and only one file to be sent with its name</response>
    /// <response code="409">The dataset with the given name already exists</response>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<ActionResult> PostNewDataset(CancellationToken cancellationToken)
    {
      if (Request.Form == null || Request.Form.Files == null || Request.Form.Files.Count == 0)
        throw new BadRequestException($"File has not been sent");
      if (Request.Form.Files.Count > 1)
        throw new BadRequestException($"Only one file can be send at once");

      bool hasName = Request.Form.TryGetValue(NewDataset.NameKey, out StringValues datasetName);
      if (!hasName)
        throw new BadRequestException($"No name of the file in the request");

      await fMediator.Send(new CreateDatasetCommand { File = Request.Form.Files[0], Name = datasetName }, cancellationToken);
      return CreatedAtRoute(nameof(GetDataset), new { name = datasetName }, datasetName);
    }

    /// <summary>
    /// Get name of all datasets already in our system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Names of all dataset in our system </returns>
    [HttpGet]
    public async Task<DatasetNames> GetNameOfDatasets(CancellationToken cancellationToken)
    {
      return await fMediator.Send(new GetDatasetNamesCommand { }, cancellationToken);
    }

    /// <summary>
    /// Get statistics for the named dataset
    /// </summary>
    /// <param name="name">Name of dataset for the</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Average number of friends and number of users in that dataset</returns>
    /// <response code="400">Name is missing in the request</response>
    /// <response code="404">Requested name was not found</response>
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [HttpGet, Route("{name}", Name = nameof(GetDataset))]
    public async Task<DatasetStatistics> GetDataset(string name, CancellationToken cancellationToken)
    {
      if (string.IsNullOrEmpty(name))
        throw new BadRequestException($"Please specify the name of the dataset");

      return await fMediator.Send(new GetDatasetStatisticsCommand { Name = name }, cancellationToken);
    }
  }
}
