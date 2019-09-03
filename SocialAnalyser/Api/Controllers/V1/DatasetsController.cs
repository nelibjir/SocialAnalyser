using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
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

    // GET: api/<controller>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<controller>/5
    [HttpGet("{name}", Name = nameof(GetDataset))]
    public string GetDataset(string name)
    {
      return "value";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]                        //[FromBody]NewDataset body, 
    public async Task<ActionResult> PostNewDataset(CancellationToken cancellationToken)
    {
      Request.Form.TryGetValue("Name", out StringValues tmp);

      await fMediator.Send(new CreateDataSetCommand { File = Request.Form.Files[0], Name = tmp }, cancellationToken);
      return CreatedAtRoute(nameof(GetDataset), tmp);
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
