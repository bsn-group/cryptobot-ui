using System.Linq;

namespace CryptobotUi.Controllers.Cryptodb
{
    partial class FuturesPnlsController
    {
        partial void OnFuturesPnlsRead(ref IQueryable<Models.Cryptodb.FuturesPnl> items)
        {
            items = items.OrderByDescending(p => p.signal_id);
        }
    }
}