using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("exchange_order", Schema = "public")]
  public partial class ExchangeOrder
  {
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("@odata.etag")]
    public string ETag
    {
        get;
        set;
    }

    [ConcurrencyCheck]
    public decimal price
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 signal_id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal original_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? executed_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 exchange_id
    {
      get;
      set;
    }
    [Key]
    public Int64 id
    {
      get;
      set;
    }

    public IEnumerable<FuturesSignalCommand> FuturesSignalCommands { get; set; }
    [ConcurrencyCheck]
    public decimal? fee_amount
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? executed_price
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime created_time
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime updated_time
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 last_trade_id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string status_reason
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string symbol
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string exchange_order_id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string exchange_order_id_secondary
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string status
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string order_side
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string fee_currency
    {
      get;
      set;
    }
  }
}
