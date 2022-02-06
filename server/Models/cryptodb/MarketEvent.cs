using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("market_event", Schema = "public")]
  public partial class MarketEvent
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
    public Int64 id
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime event_time
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
    public Int64 time_frame
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal? contracts
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64? exchange
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string source
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string name
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string message
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
    public string market
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string category
    {
      get;
      set;
    }
  }
}
