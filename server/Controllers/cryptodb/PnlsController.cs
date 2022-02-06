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

  [ODataRoutePrefix("odata/cryptodb/Pnls")]
  [Route("mvc/odata/cryptodb/Pnls")]
  public partial class PnlsController : ODataController
  {
    private Data.CryptodbContext context;

    public PnlsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/Pnls
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.Pnl> GetPnls()
    {
      var items = this.context.Pnls.AsNoTracking().AsQueryable<Models.Cryptodb.Pnl>();
      this.OnPnlsRead(ref items);

      return items;
    }

    partial void OnPnlsRead(ref IQueryable<Models.Cryptodb.Pnl> items);

    partial void OnPnlGet(ref SingleResult<Models.Cryptodb.Pnl> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<Pnl> GetPnl(Int64? key)
    {
        var items = this.context.Pnls.AsNoTracking().Where(i=>i.signal_id == key);
        var result = SingleResult.Create(items);

        OnPnlGet(ref result);

        return result;
    }
  }
}
