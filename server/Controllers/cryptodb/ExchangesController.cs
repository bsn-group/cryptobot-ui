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

  [ODataRoutePrefix("odata/cryptodb/Exchanges")]
  [Route("mvc/odata/cryptodb/Exchanges")]
  public partial class ExchangesController : ODataController
  {
    private Data.CryptodbContext context;

    public ExchangesController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Exchanges
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Exchange> GetExchanges()
    {
      var items = this.context.Exchanges.AsQueryable<Models.Cryptodb.Exchange>();
      this.OnExchangesRead(ref items);

      return items;
    }

    partial void OnExchangesRead(ref IQueryable<Models.Cryptodb.Exchange> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<Exchange> GetExchange(Int64 key)
    {
        var items = this.context.Exchanges.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnExchangeDeleted(Models.Cryptodb.Exchange item);
    partial void OnAfterExchangeDeleted(Models.Cryptodb.Exchange item);

    [HttpDelete("{id}")]
    public IActionResult DeleteExchange(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Exchanges
                .Where(i => i.id == key)
                .Include(i => i.FuturesSignals)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Exchange>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnExchangeDeleted(item);
            this.context.Exchanges.Remove(item);
            this.context.SaveChanges();
            this.OnAfterExchangeDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnExchangeUpdated(Models.Cryptodb.Exchange item);
    partial void OnAfterExchangeUpdated(Models.Cryptodb.Exchange item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutExchange(Int64 key, [FromBody]Models.Cryptodb.Exchange newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Exchanges
                .Where(i => i.id == key)
                .Include(i => i.FuturesSignals)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Exchange>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnExchangeUpdated(newItem);
            this.context.Exchanges.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Exchanges.Where(i => i.id == key);
            this.OnAfterExchangeUpdated(newItem);
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
    public IActionResult PatchExchange(Int64 key, [FromBody]Delta<Models.Cryptodb.Exchange> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Exchanges.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.Exchange>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnExchangeUpdated(item);
            this.context.Exchanges.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Exchanges.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnExchangeCreated(Models.Cryptodb.Exchange item);
    partial void OnAfterExchangeCreated(Models.Cryptodb.Exchange item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.Exchange item)
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

            this.OnExchangeCreated(item);
            this.context.Exchanges.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Cryptodb/Exchanges/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
