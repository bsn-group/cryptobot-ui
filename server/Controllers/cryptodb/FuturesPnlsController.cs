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

  [ODataRoutePrefix("odata/cryptodb/FuturesPnls")]
  [Route("mvc/odata/cryptodb/FuturesPnls")]
  public partial class FuturesPnlsController : ODataController
  {
    private Data.CryptodbContext context;

    public FuturesPnlsController(Data.CryptodbContext context)
    {
      this.context = context;
    }
    // GET /odata/Cryptodb/FuturesPnls
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Cryptodb.FuturesPnl> GetFuturesPnls()
    {
      var items = this.context.FuturesPnls.AsNoTracking().AsQueryable<Models.Cryptodb.FuturesPnl>();
      this.OnFuturesPnlsRead(ref items);

      return items;
    }

    partial void OnFuturesPnlsRead(ref IQueryable<Models.Cryptodb.FuturesPnl> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{signal_id}")]
    public SingleResult<FuturesPnl> GetFuturesPnl(Int64? key)
    {
        var items = this.context.FuturesPnls.AsNoTracking().Where(i=>i.signal_id == key);
        return SingleResult.Create(items);
    }
  }
}
