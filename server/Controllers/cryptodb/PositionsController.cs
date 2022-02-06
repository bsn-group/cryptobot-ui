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

  [ODataRoutePrefix("odata/cryptodb/Positions")]
  [Route("mvc/odata/cryptodb/Positions")]
  public partial class PositionsController : ODataController
  {
    private Data.CryptodbContext context;

    public PositionsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Positions
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Position> GetPositions()
    {
      var items = this.context.Positions.AsNoTracking().AsQueryable<Models.Cryptodb.Position>();
      this.OnPositionsRead(ref items);

      return items;
    }

    partial void OnPositionsRead(ref IQueryable<Models.Cryptodb.Position> items);

    partial void OnPositionGet(ref SingleResult<Models.Cryptodb.Position> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<Position> GetPosition(Int64? key)
    {
        var items = this.context.Positions.AsNoTracking().Where(i=>i.signal_id == key);
        var result = SingleResult.Create(items);

        OnPositionGet(ref result);

        return result;
    }
  }
}
