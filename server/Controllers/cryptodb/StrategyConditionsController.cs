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

  [ODataRoutePrefix("odata/cryptodb/StrategyConditions")]
  [Route("mvc/odata/cryptodb/StrategyConditions")]
  public partial class StrategyConditionsController : ODataController
  {
    private Data.CryptodbContext context;

    public StrategyConditionsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/StrategyConditions
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.StrategyCondition> GetStrategyConditions()
    {
      var items = this.context.StrategyConditions.AsQueryable<Models.Cryptodb.StrategyCondition>();
      this.OnStrategyConditionsRead(ref items);

      return items;
    }

    partial void OnStrategyConditionsRead(ref IQueryable<Models.Cryptodb.StrategyCondition> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<StrategyCondition> GetStrategyCondition(Int64 key)
    {
        var items = this.context.StrategyConditions.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnStrategyConditionDeleted(Models.Cryptodb.StrategyCondition item);
    partial void OnAfterStrategyConditionDeleted(Models.Cryptodb.StrategyCondition item);

    [HttpDelete("{id}")]
    public IActionResult DeleteStrategyCondition(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.StrategyConditions
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.StrategyCondition>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnStrategyConditionDeleted(item);
            this.context.StrategyConditions.Remove(item);
            this.context.SaveChanges();
            this.OnAfterStrategyConditionDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStrategyConditionUpdated(Models.Cryptodb.StrategyCondition item);
    partial void OnAfterStrategyConditionUpdated(Models.Cryptodb.StrategyCondition item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutStrategyCondition(Int64 key, [FromBody]Models.Cryptodb.StrategyCondition newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.StrategyConditions
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.StrategyCondition>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnStrategyConditionUpdated(newItem);
            this.context.StrategyConditions.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.StrategyConditions.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Strategy");
            this.OnAfterStrategyConditionUpdated(newItem);
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
    public IActionResult PatchStrategyCondition(Int64 key, [FromBody]Delta<Models.Cryptodb.StrategyCondition> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.StrategyConditions.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.StrategyCondition>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnStrategyConditionUpdated(item);
            this.context.StrategyConditions.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.StrategyConditions.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Strategy");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnStrategyConditionCreated(Models.Cryptodb.StrategyCondition item);
    partial void OnAfterStrategyConditionCreated(Models.Cryptodb.StrategyCondition item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.StrategyCondition item)
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

            this.OnStrategyConditionCreated(item);
            this.context.StrategyConditions.Add(item);
            this.context.SaveChanges();

            var key = item.id;

            var itemToReturn = this.context.StrategyConditions.Where(i => i.id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Strategy");

            this.OnAfterStrategyConditionCreated(item);

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
