using CryptobotUi.Models.Cryptodb;
using Microsoft.EntityFrameworkCore;

namespace CryptobotUi.Data
{
    partial class CryptodbContext
    {
        partial void OnModelBuilding(ModelBuilder builder)
        {
            builder.Entity<ExchangeOrder>()
                .HasOne(exo => exo.Signal)
                .WithMany(s => s.ExchangeOrders)
                .HasForeignKey(exo => exo.signal_id);
        }
    }
}