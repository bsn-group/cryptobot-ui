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

  [ODataRoutePrefix("odata/cryptodb/ExchangeOrders")]
  [Route("mvc/odata/cryptodb/ExchangeOrders")]
  public partial class ExchangeOrdersController : ODataController
  {
    private Data.CryptodbContext context;

    public ExchangeOrdersController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/ExchangeOrders
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.ExchangeOrder> GetExchangeOrders()
    {
      var items = this.context.ExchangeOrders.AsQueryable<Models.Cryptodb.ExchangeOrder>();
      this.OnExchangeOrdersRead(ref items);

      return items;
    }

    partial void OnExchangeOrdersRead(ref IQueryable<Models.Cryptodb.ExchangeOrder> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{id}")]
    public SingleResult<ExchangeOrder> GetExchangeOrder(Int64 key)
    {
        var items = this.context.ExchangeOrders.Where(i=>i.id == key);
        return SingleResult.Create(items);
    }
    partial void OnExchangeOrderDeleted(Models.Cryptodb.ExchangeOrder item);
    partial void OnAfterExchangeOrderDeleted(Models.Cryptodb.ExchangeOrder item);

    [HttpDelete("{id}")]
    public IActionResult DeleteExchangeOrder(Int64 key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.ExchangeOrders
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.ExchangeOrder>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnExchangeOrderDeleted(item);
            this.context.ExchangeOrders.Remove(item);
            this.context.SaveChanges();
            this.OnAfterExchangeOrderDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnExchangeOrderUpdated(Models.Cryptodb.ExchangeOrder item);
    partial void OnAfterExchangeOrderUpdated(Models.Cryptodb.ExchangeOrder item);

    [HttpPut("{id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutExchangeOrder(Int64 key, [FromBody]Models.Cryptodb.ExchangeOrder newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ExchangeOrders
                .Where(i => i.id == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.ExchangeOrder>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnExchangeOrderUpdated(newItem);
            this.context.ExchangeOrders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.ExchangeOrders.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignalCommand");
            this.OnAfterExchangeOrderUpdated(newItem);
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
    public IActionResult PatchExchangeOrder(Int64 key, [FromBody]Delta<Models.Cryptodb.ExchangeOrder> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ExchangeOrders.Where(i => i.id == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.ExchangeOrder>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnExchangeOrderUpdated(item);
            this.context.ExchangeOrders.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.ExchangeOrders.Where(i => i.id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignalCommand");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnExchangeOrderCreated(Models.Cryptodb.ExchangeOrder item);
    partial void OnAfterExchangeOrderCreated(Models.Cryptodb.ExchangeOrder item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.ExchangeOrder item)
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

            this.OnExchangeOrderCreated(item);
            this.context.ExchangeOrders.Add(item);
            this.context.SaveChanges();

            var key = item.id;

            var itemToReturn = this.context.ExchangeOrders.Where(i => i.id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "FuturesSignalCommand");

            this.OnAfterExchangeOrderCreated(item);

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
