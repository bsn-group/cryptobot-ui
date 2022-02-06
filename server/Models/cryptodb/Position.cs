using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("positions", Schema = "public")]
  public partial class Position
  {
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("@odata.etag")]
    public string ETag
    {
        get;
        set;
    }

    [Key]
    public Int64? signal_id
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
    public string position_type
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64? exchange_id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string strategy_pair_name
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string signal_status
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string position_status
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? executed_buy_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? pending_buy_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? executed_sell_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? pending_sell_qty
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64? open_commands_count
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64? close_commands_count
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64? pending_commands_count
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? entry_price
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? close_price
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? entry_time
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? exit_time
    {
      get;
      set;
    }
  }
}
