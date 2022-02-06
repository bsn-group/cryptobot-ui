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

  [ODataRoutePrefix("odata/cryptodb/Symbols")]
  [Route("mvc/odata/cryptodb/Symbols")]
  public partial class SymbolsController : ODataController
  {
    private Data.CryptodbContext context;

    public SymbolsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Symbols
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Symbol> GetSymbols()
    {
      var items = this.context.Symbols.AsQueryable<Models.Cryptodb.Symbol>();
      this.OnSymbolsRead(ref items);

      return items;
    }

    partial void OnSymbolsRead(ref IQueryable<Models.Cryptodb.Symbol> items);

    partial void OnSymbolGet(ref SingleResult<Models.Cryptodb.Symbol> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{name}")]
    public SingleResult<Symbol> GetSymbol(string key)
    {
        var items = this.context.Symbols.Where(i=>i.name == key);
        var result = SingleResult.Create(items);

        OnSymbolGet(ref result);

        return result;
    }
    partial void OnSymbolDeleted(Models.Cryptodb.Symbol item);
    partial void OnAfterSymbolDeleted(Models.Cryptodb.Symbol item);

    [HttpDelete("{name}")]
    public IActionResult DeleteSymbol(string key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Symbols
                .Where(i => i.name == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Symbol>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSymbolDeleted(item);
            this.context.Symbols.Remove(item);
            this.context.SaveChanges();
            this.OnAfterSymbolDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSymbolUpdated(Models.Cryptodb.Symbol item);
    partial void OnAfterSymbolUpdated(Models.Cryptodb.Symbol item);

    [HttpPut("{name}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutSymbol(string key, [FromBody]Models.Cryptodb.Symbol newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Symbols
                .Where(i => i.name == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Symbol>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnSymbolUpdated(newItem);
            this.context.Symbols.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Symbols.Where(i => i.name == key);
            this.OnAfterSymbolUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{name}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchSymbol(string key, [FromBody]Delta<Models.Cryptodb.Symbol> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Symbols.Where(i => i.name == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.Symbol>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnSymbolUpdated(item);
            this.context.Symbols.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Symbols.Where(i => i.name == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSymbolCreated(Models.Cryptodb.Symbol item);
    partial void OnAfterSymbolCreated(Models.Cryptodb.Symbol item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.Symbol item)
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

            this.OnSymbolCreated(item);
            this.context.Symbols.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Cryptodb/Symbols/{item.name}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
