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

  [ODataRoutePrefix("odata/cryptodb/Signals")]
  [Route("mvc/odata/cryptodb/Signals")]
  public partial class SignalsController : ODataController
  {
    private Data.CryptodbContext context;

    public SignalsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Signals
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Signal> GetSignals()
    {
      var items = this.context.Signals.AsQueryable<Models.Cryptodb.Signal>();
      this.OnSignalsRead(ref items);

      return items;
    }

    partial void OnSignalsRead(ref IQueryable<Models.Cryptodb.Signal> items);

    partial void OnSignalGet(ref SingleResult<Models.Cryptodb.Signal> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<Signal> GetSignal(Int64 key)
    {
        var items = this.context.Signals.Where(i=>i.signal_id == key);
        var result = SingleResult.Create(items);

        OnSignalGet(ref result);

        return result;
    }
    partial void OnSignalDeleted(Models.Cryptodb.Signal item);
    partial void OnAfterSignalDeleted(Models.Cryptodb.Signal item);

    [HttpDelete("{signal_id}")]
    public IActionResult DeleteSignal(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Signals
                .Where(i => i.signal_id == key)
                .Include(i => i.SignalCommands)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Signal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSignalDeleted(item);
            this.context.Signals.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSignalDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSignalUpdated(Models.Cryptodb.Signal item);
    partial void OnAfterSignalUpdated(Models.Cryptodb.Signal item);

    [HttpPut("{signal_id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSignal(Int64 key, [FromBody]Models.Cryptodb.Signal newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Signals
                .Where(i => i.signal_id == key)
                .Include(i => i.SignalCommands)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Signal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSignalUpdated(newItem);
            this.context.Signals.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Signals.Where(i => i.signal_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Exchange,Strategy");
            this.OnAfterSignalUpdated(newItem);
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
    public IActionResult PatchSignal(Int64 key, [FromBody]Delta<Models.Cryptodb.Signal> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Signals.Where(i => i.signal_id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.Signal>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnSignalUpdated(item);
            this.context.Signals.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Signals.Where(i => i.signal_id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Exchange,Strategy");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSignalCreated(Models.Cryptodb.Signal item);
    partial void OnAfterSignalCreated(Models.Cryptodb.Signal item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.Signal item)
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

            this.OnSignalCreated(item);
            this.context.Signals.Add(item);
            this.context.SaveChanges();

            var key = item.signal_id;

            var itemToReturn = this.context.Signals.Where(i => i.signal_id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Exchange,Strategy");

            this.OnAfterSignalCreated(item);

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
