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

  [ODataRoutePrefix("odata/cryptodb/FuturesSignals")]
  [Route("mvc/odata/cryptodb/FuturesSignals")]
  public partial class FuturesSignalsController : ODataController
  {
    private Data.CryptodbContext context;

    public FuturesSignalsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/FuturesSignals
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.FuturesSignal> GetFuturesSignals()
    {
      var items = this.context.FuturesSignals.AsQueryable<Models.Cryptodb.FuturesSignal>();
      this.OnFuturesSignalsRead(ref items);

      return items;
    }

    partial void OnFuturesSignalsRead(ref IQueryable<Models.Cryptodb.FuturesSignal> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<FuturesSignal> GetFuturesSignal(Int64 key)
    {
        var items = this.context.FuturesSignals.Where(i=>i.signal_id == key);
        return SingleResult.Create(items);
    }
    partial void OnFuturesSignalDeleted(Models.Cryptodb.FuturesSignal item);
    partial void OnAfterFuturesSignalDeleted(Models.Cryptodb.FuturesSignal item);

    [HttpDelete("{signal_id}")]
    public IActionResult DeleteFuturesSignal(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.FuturesSignals
                .Where(i => i.signal_id == key)
                .Include(i => i.FuturesSignalCommands)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFuturesSignalDeleted(item);
            this.context.FuturesSignals.Remove(item);
            this.context.SaveChanges();
            this.OnAfterFuturesSignalDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFuturesSignalUpdated(Models.Cryptodb.FuturesSignal item);
    partial void OnAfterFuturesSignalUpdated(Models.Cryptodb.FuturesSignal item);

    [HttpPut("{signal_id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutFuturesSignal(Int64 key, [FromBody]Models.Cryptodb.FuturesSignal newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FuturesSignals
                .Where(i => i.signal_id == key)
                .Include(i => i.FuturesSignalCommands)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFuturesSignalUpdated(newItem);
            this.context.FuturesSignals.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.FuturesSignals.Where(i => i.signal_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Exchange");
            this.OnAfterFuturesSignalUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{signal_id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchFuturesSignal(Int64 key, [FromBody]Delta<Models.Cryptodb.FuturesSignal> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FuturesSignals.Where(i => i.signal_id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.FuturesSignal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnFuturesSignalUpdated(item);
            this.context.FuturesSignals.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.FuturesSignals.Where(i => i.signal_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Exchange");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFuturesSignalCreated(Models.Cryptodb.FuturesSignal item);
    partial void OnAfterFuturesSignalCreated(Models.Cryptodb.FuturesSignal item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.FuturesSignal item)
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

            this.OnFuturesSignalCreated(item);
            this.context.FuturesSignals.Add(item);
            this.context.SaveChanges();

            var key = item.signal_id;

            var itemToReturn = this.context.FuturesSignals.Where(i => i.signal_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Exchange");

            this.OnAfterFuturesSignalCreated(item);

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
