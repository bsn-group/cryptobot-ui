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

  [ODataRoutePrefix("odata/cryptodb/FuturesPositions")]
  [Route("mvc/odata/cryptodb/FuturesPositions")]
  public partial class FuturesPositionsController : ODataController
  {
    private Data.CryptodbContext context;

    public FuturesPositionsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/FuturesPositions
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.FuturesPosition> GetFuturesPositions()
    {
      var items = this.context.FuturesPositions.AsNoTracking().AsQueryable<Models.Cryptodb.FuturesPosition>();
      this.OnFuturesPositionsRead(ref items);

      return items;
    }

    partial void OnFuturesPositionsRead(ref IQueryable<Models.Cryptodb.FuturesPosition> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<FuturesPosition> GetFuturesPosition(Int64? key)
    {
        var items = this.context.FuturesPositions.AsNoTracking().Where(i=>i.signal_id == key);
        return SingleResult.Create(items);
    }
  }
}
