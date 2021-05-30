using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNet.OData.Query;



namespace CryptobotUi.Controllers.Cryptodb
{
  using Models;
  using Data;
  using Models.Cryptodb;

  [ODataRoutePrefix("odata/cryptodb/FuturesSignalCommands")]
  [Route("mvc/odata/cryptodb/FuturesSignalCommands")]
  public partial class FuturesSignalCommandsController : ODataController
  {
    private Data.CryptodbContext context;

    public FuturesSignalCommandsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/FuturesSignalCommands
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.FuturesSignalCommand> GetFuturesSignalCommands()
    {
      var items = this.context.FuturesSignalCommands.AsQueryable<Models.Cryptodb.FuturesSignalCommand>();
      this.OnFuturesSignalCommandsRead(ref items);

      return items;
    }

    partial void OnFuturesSignalCommandsRead(ref IQueryable<Models.Cryptodb.FuturesSignalCommand> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<FuturesSignalCommand> GetFuturesSignalCommand(Int64 key)
    {
        var items = this.context.FuturesSignalCommands.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnFuturesSignalCommandDeleted(Models.Cryptodb.FuturesSignalCommand item);
    partial void OnAfterFuturesSignalCommandDeleted(Models.Cryptodb.FuturesSignalCommand item);

    [HttpDelete("{id}")]
    public IActionResult DeleteFuturesSignalCommand(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.FuturesSignalCommands
                .Where(i => i.id == key)
                .Include(i => i.ExchangeOrders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFuturesSignalCommandDeleted(item);
            this.context.FuturesSignalCommands.Remove(item);
            this.context.SaveChanges();
            this.OnAfterFuturesSignalCommandDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFuturesSignalCommandUpdated(Models.Cryptodb.FuturesSignalCommand item);
    partial void OnAfterFuturesSignalCommandUpdated(Models.Cryptodb.FuturesSignalCommand item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutFuturesSignalCommand(Int64 key, [FromBody]Models.Cryptodb.FuturesSignalCommand newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FuturesSignalCommands
                .Where(i => i.id == key)
                .Include(i => i.ExchangeOrders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFuturesSignalCommandUpdated(newItem);
            this.context.FuturesSignalCommands.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.FuturesSignalCommands.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignal");
            this.OnAfterFuturesSignalCommandUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchFuturesSignalCommand(Int64 key, [FromBody]Delta<Models.Cryptodb.FuturesSignalCommand> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FuturesSignalCommands.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnFuturesSignalCommandUpdated(item);
            this.context.FuturesSignalCommands.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.FuturesSignalCommands.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignal");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFuturesSignalCommandCreated(Models.Cryptodb.FuturesSignalCommand item);
    partial void OnAfterFuturesSignalCommandCreated(Models.Cryptodb.FuturesSignalCommand item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.FuturesSignalCommand item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnFuturesSignalCommandCreated(item);
            this.context.FuturesSignalCommands.Add(item);
            this.context.SaveChanges();

            var key = item.id;

            var itemToReturn = this.context.FuturesSignalCommands.Where(i => i.id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignal");

            this.OnAfterFuturesSignalCommandCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
