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

  [ODataRoutePrefix("odata/cryptodb/Configs")]
  [Route("mvc/odata/cryptodb/Configs")]
  public partial class ConfigsController : ODataController
  {
    private Data.CryptodbContext context;

    public ConfigsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Configs
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Config> GetConfigs()
    {
      var items = this.context.Configs.AsQueryable<Models.Cryptodb.Config>();
      this.OnConfigsRead(ref items);

      return items;
    }

    partial void OnConfigsRead(ref IQueryable<Models.Cryptodb.Config> items);

    partial void OnConfigGet(ref SingleResult<Models.Cryptodb.Config> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{name}")]
    public SingleResult<Config> GetConfig(string key)
    {
        var items = this.context.Configs.Where(i=>i.name == key);
        var result = SingleResult.Create(items);

        OnConfigGet(ref result);

        return result;
    }
    partial void OnConfigDeleted(Models.Cryptodb.Config item);
    partial void OnAfterConfigDeleted(Models.Cryptodb.Config item);

    [HttpDelete("{name}")]
    public IActionResult DeleteConfig(string key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Configs
                .Where(i => i.name == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Config>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnConfigDeleted(item);
            this.context.Configs.Remove(item);
            this.context.SaveChanges();
            this.OnAfterConfigDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnConfigUpdated(Models.Cryptodb.Config item);
    partial void OnAfterConfigUpdated(Models.Cryptodb.Config item);

    [HttpPut("{name}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutConfig(string key, [FromBody]Models.Cryptodb.Config newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Configs
                .Where(i => i.name == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Cryptodb.Config>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnConfigUpdated(newItem);
            this.context.Configs.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Configs.Where(i => i.name == key);
            this.OnAfterConfigUpdated(newItem);
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
    public IActionResult PatchConfig(string key, [FromBody]Delta<Models.Cryptodb.Config> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Configs.Where(i => i.name == key);

            items = EntityPatch.ApplyTo<Models.Cryptodb.Config>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnConfigUpdated(item);
            this.context.Configs.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Configs.Where(i => i.name == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnConfigCreated(Models.Cryptodb.Config item);
    partial void OnAfterConfigCreated(Models.Cryptodb.Config item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Cryptodb.Config item)
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

            this.OnConfigCreated(item);
            this.context.Configs.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Cryptodb/Configs/{item.name}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
