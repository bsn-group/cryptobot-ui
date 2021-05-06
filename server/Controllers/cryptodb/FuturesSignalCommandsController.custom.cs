using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace  CryptobotUi.Controllers.Cryptodb
{
    partial class FuturesSignalCommandsController
    {
        partial void OnFuturesSignalCommandsRead(ref IQueryable<Models.Cryptodb.FuturesSignalCommand> items)
        {
            if (items != null)
            {
                items = items.Include(fsc => fsc.ExchangeOrder);
            }
        }
    }
}