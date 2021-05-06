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

  [ODataRoutePrefix("odata/cryptodb/Strategies")]
  [Route("mvc/odata/cryptodb/Strategies")]
  public partial class StrategiesController : ODataController
  {
    private Data.CryptodbContext context;

    public StrategiesController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Strategies
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Strategy> GetStrategies()
    {
      var items = this.context.Strategies.AsQueryable<Models.Cryptodb.Strategy>();
      this.OnStrategiesRead(ref items);

      return items;
    }

    partial void OnStrategiesRead(ref IQueryable<Models.Cryptodb.Strategy> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<Strategy> GetStrategy(Int64 key)
    {
        var items = this.context.Strategies.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnStrategyDeleted(Models.Cryptodb.Strategy item);
    partial void OnAfterStrategyDeleted(Models.Cryptodb.Strategy item);

    [HttpDelete("{id}")]
    public IActionResult DeleteStrategy(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Strategies
                .Where(i => i.id == key)
                .Include(i => i.StrategyConditions)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Strategy>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnStrategyDeleted(item);
            this.context.Strategies.Remove(item);
            this.context.SaveChanges();
            this.OnAfterStrategyDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStrategyUpdated(Models.Cryptodb.Strategy item);
    partial void OnAfterStrategyUpdated(Models.Cryptodb.Strategy item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutStrategy(Int64 key, [FromBody]Models.Cryptodb.Strategy newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Strategies
                .Where(i => i.id == key)
                .Include(i => i.StrategyConditions)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Strategy>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnStrategyUpdated(newItem);
            this.context.Strategies.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Strategies.Where(i => i.id == key);
            this.OnAfterStrategyUpdated(newItem);
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
    public IActionResult PatchStrategy(Int64 key, [FromBody]Delta<Models.Cryptodb.Strategy> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Strategies.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.Strategy>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnStrategyUpdated(item);
            this.context.Strategies.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Strategies.Where(i => i.id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStrategyCreated(Models.Cryptodb.Strategy item);
    partial void OnAfterStrategyCreated(Models.Cryptodb.Strategy item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.Strategy item)
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

            this.OnStrategyCreated(item);
            this.context.Strategies.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Cryptodb/Strategies/{item.id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
