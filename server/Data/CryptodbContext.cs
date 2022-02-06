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

        builder.Entity<CryptobotUi.Models.Cryptodb.Pnl>().HasNoKey();
        builder.Entity<CryptobotUi.Models.Cryptodb.Position>().HasNoKey();
        builder.Entity<CryptobotUi.Models.Cryptodb.ExchangeOrder>()
              .HasOne(i => i.SignalCommand)
              .WithMany(i => i.ExchangeOrders)
              .HasForeignKey(i => i.signal_command_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.Signal>()
              .HasOne(i => i.Exchange)
              .WithMany(i => i.Signals)
              .HasForeignKey(i => i.exchange_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.Signal>()
              .HasOne(i => i.Strategy)
              .WithMany(i => i.Signals)
              .HasForeignKey(i => i.strategy_pair_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.SignalCommand>()
              .HasOne(i => i.StrategyCondition)
              .WithMany(i => i.SignalCommands)
              .HasForeignKey(i => i.strategy_condition_id)
              .HasPrincipalKey(i => i.id);
        builder.Entity<CryptobotUi.Models.Cryptodb.SignalCommand>()
              .HasOne(i => i.Signal)
              .WithMany(i => i.SignalCommands)
              .HasForeignKey(i => i.signal_id)
              .HasPrincipalKey(i => i.signal_id);
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
              .HasDefaultValueSql("nextval('exchange_order_id_seq'::regclass)").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.ExchangeOrder>()
              .Property(p => p.last_trade_id)
              .HasDefaultValueSql("0").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.Signal>()
              .Property(p => p.exchange_id)
              .HasDefaultValueSql("1").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.Signal>()
              .Property(p => p.strategy_pair_id)
              .HasDefaultValueSql("nextval('signal_strategy_pair_id_seq'::regclass)").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.SignalCommand>()
              .Property(p => p.strategy_condition_id)
              .HasDefaultValueSql("nextval('signal_command_strategy_condition_id_seq'::regclass)").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.Strategy>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('strategy_id_seq'::regclass)").ValueGeneratedNever();

        builder.Entity<CryptobotUi.Models.Cryptodb.StrategyCondition>()
              .Property(p => p.id)
              .HasDefaultValueSql("nextval('strategy_conditions_id_seq'::regclass)").ValueGeneratedNever();

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

    public DbSet<CryptobotUi.Models.Cryptodb.MarketEvent> MarketEvents
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Pnl> Pnls
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Position> Positions
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.Signal> Signals
    {
      get;
      set;
    }

    public DbSet<CryptobotUi.Models.Cryptodb.SignalCommand> SignalCommands
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
