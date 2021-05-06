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

  [ODataRoutePrefix("odata/cryptodb/MarketEvents")]
  [Route("mvc/odata/cryptodb/MarketEvents")]
  public partial class MarketEventsController : ODataController
  {
    private Data.CryptodbContext context;

    public MarketEventsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/MarketEvents
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.MarketEvent> GetMarketEvents()
    {
      var items = this.context.MarketEvents.AsQueryable<Models.Cryptodb.MarketEvent>();
      this.OnMarketEventsRead(ref items);

      return items;
    }

    partial void OnMarketEventsRead(ref IQueryable<Models.Cryptodb.MarketEvent> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<MarketEvent> GetMarketEvent(Int64 key)
    {
        var items = this.context.MarketEvents.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnMarketEventDeleted(Models.Cryptodb.MarketEvent item);
    partial void OnAfterMarketEventDeleted(Models.Cryptodb.MarketEvent item);

    [HttpDelete("{id}")]
    public IActionResult DeleteMarketEvent(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.MarketEvents
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.MarketEvent>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnMarketEventDeleted(item);
            this.context.MarketEvents.Remove(item);
            this.context.SaveChanges();
            this.OnAfterMarketEventDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnMarketEventUpdated(Models.Cryptodb.MarketEvent item);
    partial void OnAfterMarketEventUpdated(Models.Cryptodb.MarketEvent item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutMarketEvent(Int64 key, [FromBody]Models.Cryptodb.MarketEvent newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.MarketEvents
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.MarketEvent>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnMarketEventUpdated(newItem);
            this.context.MarketEvents.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.MarketEvents.Where(i => i.id == key);
            this.OnAfterMarketEventUpdated(newItem);
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
    public IActionResult PatchMarketEvent(Int64 key, [FromBody]Delta<Models.Cryptodb.MarketEvent> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.MarketEvents.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.MarketEvent>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnMarketEventUpdated(item);
            this.context.MarketEvents.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.MarketEvents.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnMarketEventCreated(Models.Cryptodb.MarketEvent item);
    partial void OnAfterMarketEventCreated(Models.Cryptodb.MarketEvent item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.MarketEvent item)
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

            this.OnMarketEventCreated(item);
            this.context.MarketEvents.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Cryptodb/MarketEvents/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
