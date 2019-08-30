﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialAnalyser.Commands;

namespace SocialAnalyser.Api.Controllers.V1
{
  [Route("api/v1/[controller]")]
  public class DatasetsController: Controller
  {

    private readonly IMediator fMediator;

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
    [HttpGet("{id}", Name = nameof(GetDataset))]
    public string GetDataset(int id)
    {
      return "value";
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<ActionResult> PostNewDataset([FromBody]string body, CancellationToken cancellationToken)
    {
      await fMediator.Send(new CreateDataSetCommand { Dataset = body }, cancellationToken);
      return CreatedAtRoute(nameof(GetDataset), null);
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
