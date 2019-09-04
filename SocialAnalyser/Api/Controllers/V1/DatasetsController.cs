using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SocialAnalyser.Api.Models;
using SocialAnalyser.Commands;

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
    /// 
    /// </summary>
    /// <param name="name">Name of dataset for the</param>
    /// <returns>Average number of friends and number of users in that dataset</returns>
    [HttpGet, Route("{name}", Name = nameof(GetDataset))]
    public async Task<DatasetStatistics> GetDataset(string name, CancellationToken cancellationToken)
    {
      if (string.IsNullOrEmpty(name))
        return null; // throw exception and in middleware then given status code

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
      Request.Form.TryGetValue(NewDataset.NameKey, out StringValues datasetName);

      await fMediator.Send(new CreateDatasetCommand { File = Request.Form.Files[0], Name = datasetName }, cancellationToken);
      return CreatedAtRoute(nameof(GetDataset), new { name = datasetName }, datasetName);
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
