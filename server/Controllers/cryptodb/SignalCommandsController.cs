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
using Microsoft.AspNet.OData.Query;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace CryptobotUi.Controllers.Cryptodb
{
  using Models;
  using Data;
  using Models.Cryptodb;

  [ODataRoutePrefix("odata/cryptodb/SignalCommands")]
  [Route("mvc/odata/cryptodb/SignalCommands")]
  public partial class SignalCommandsController : ODataController
  {
    private Data.CryptodbContext context;

    public SignalCommandsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/SignalCommands
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.SignalCommand> GetSignalCommands()
    {
      var items = this.context.SignalCommands.AsQueryable<Models.Cryptodb.SignalCommand>();
      this.OnSignalCommandsRead(ref items);

      return items;
    }

    partial void OnSignalCommandsRead(ref IQueryable<Models.Cryptodb.SignalCommand> items);

    partial void OnSignalCommandGet(ref SingleResult<Models.Cryptodb.SignalCommand> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<SignalCommand> GetSignalCommand(Int64 key)
    {
        var items = this.context.SignalCommands.Where(i=>i.id == key);
        var result = SingleResult.Create(items);

        OnSignalCommandGet(ref result);

        return result;
    }
    partial void OnSignalCommandDeleted(Models.Cryptodb.SignalCommand item);
    partial void OnAfterSignalCommandDeleted(Models.Cryptodb.SignalCommand item);

    [HttpDelete("{id}")]
    public IActionResult DeleteSignalCommand(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.SignalCommands
                .Where(i => i.id == key)
                .Include(i => i.ExchangeOrders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.SignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSignalCommandDeleted(item);
            this.context.SignalCommands.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSignalCommandDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSignalCommandUpdated(Models.Cryptodb.SignalCommand item);
    partial void OnAfterSignalCommandUpdated(Models.Cryptodb.SignalCommand item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSignalCommand(Int64 key, [FromBody]Models.Cryptodb.SignalCommand newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.SignalCommands
                .Where(i => i.id == key)
                .Include(i => i.ExchangeOrders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.SignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSignalCommandUpdated(newItem);
            this.context.SignalCommands.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.SignalCommands.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "StrategyCondition,Signal");
            this.OnAfterSignalCommandUpdated(newItem);
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
    public IActionResult PatchSignalCommand(Int64 key, [FromBody]Delta<Models.Cryptodb.SignalCommand> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.SignalCommands.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.SignalCommand>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnSignalCommandUpdated(item);
            this.context.SignalCommands.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.SignalCommands.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "StrategyCondition,Signal");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSignalCommandCreated(Models.Cryptodb.SignalCommand item);
    partial void OnAfterSignalCommandCreated(Models.Cryptodb.SignalCommand item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.SignalCommand item)
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

            this.OnSignalCommandCreated(item);
            this.context.SignalCommands.Add(item);
            this.context.SaveChanges();

            var key = item.id;

            var itemToReturn = this.context.SignalCommands.Where(i => i.id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "StrategyCondition,Signal");

            this.OnAfterSignalCommandCreated(item);

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
