using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("strategy", Schema = "public")]
  public partial class Strategy
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
    public Int64 version
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
    public DateTime created_time
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

    public IEnumerable<StrategyCondition> StrategyConditions { get; set; }
    public IEnumerable<Signal> Signals { get; set; }
    [ConcurrencyCheck]
    public Int64? exchange_id
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
    public string status
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
    public string position_type
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string exchange_type
    {
      get;
      set;
    }
  }
}
