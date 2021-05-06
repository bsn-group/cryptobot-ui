using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using CryptobotUi.Models.Cryptodb;

namespace CryptobotUi.Data
{
  public partial class CryptodbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CryptodbContext(DbContextOptions<CryptodbContext> options):base(options)
    {
    }

    public CryptodbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesPnl>().HasNoKey();
        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesPosition>().HasNoKey();
        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesSignal>()
              .HasOne(i => i.Exchange)
              .WithMany(i => i.FuturesSignals)
              .HasForeignKey(i => i.exchange_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesSignalCommand>()
              .HasOne(i => i.FuturesSignal)
              .WithMany(i => i.FuturesSignalCommands)
              .HasForeignKey(i => i.signal_id)
              .HasPrincipalKey(i => i.signal_id);
        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesSignalCommand>()
              .HasOne(i => i.ExchangeOrder)
              .WithMany(i => i.FuturesSignalCommands)
              .HasForeignKey(i => i.exchange_order_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.StrategyCondition>()
              .HasOne(i => i.Strategy)
              .WithMany(i => i.StrategyConditions)
              .HasForeignKey(i => i.strategy_id)
              .HasPrincipalKey(i => i.id);

        builder.Entity<CryptobotUi.Models.Cryptodb.Exchange>()
              .Property(p => p.active)
              .HasDefaultValueSql("true");

        builder.Entity<CryptobotUi.Models.Cryptodb.ExchangeOrder>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('exchange_order_id_seq'::regclass)");

        builder.Entity<CryptobotUi.Models.Cryptodb.ExchangeOrder>()
              .Property(p => p.last_trade_id)
              .HasDefaultValueSql("0");

        builder.Entity<CryptobotUi.Models.Cryptodb.FuturesSignal>()
              .Property(p => p.exchange_id)
              .HasDefaultValueSql("1");

        builder.Entity<CryptobotUi.Models.Cryptodb.Strategy>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('strategy_id_seq'::regclass)");

        builder.Entity<CryptobotUi.Models.Cryptodb.StrategyCondition>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('strategy_conditions_id_seq'::regclass)");

        this.OnModelBuilding(builder);
    }


    public DbSet<CryptobotUi.Models.Cryptodb.Config> Configs
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Exchange> Exchanges
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.ExchangeOrder> ExchangeOrders
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.FuturesPnl> FuturesPnls
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.FuturesPosition> FuturesPositions
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.FuturesSignal> FuturesSignals
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.FuturesSignalCommand> FuturesSignalCommands
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.MarketEvent> MarketEvents
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Strategy> Strategies
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.StrategyCondition> StrategyConditions
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Symbol> Symbols
    {
      get;
      set;
    }
  }
}
