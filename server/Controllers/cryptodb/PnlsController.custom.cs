using System.Linq;

namespace CryptobotUi.Controllers.Cryptodb
{
    partial class PnlsController
    {
        partial void OnPnlsRead(ref IQueryable<Models.Cryptodb.Pnl> items)
        {
            items = items.OrderByDescending(p => p.signal_id);
        }
    }
}