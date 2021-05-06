using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("strategy_conditions", Schema = "public")]
  public partial class StrategyCondition
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
    public DateTime created_time
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 strategy_id
    {
      get;
      set;
    }
    public Strategy Strategy { get; set; }
    [ConcurrencyCheck]
    public Int64 time_frame
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 last_observed
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public Int64 sequence
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
    public Int64 condition_sub_group
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
    public string category
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
    public string condition_group
    {
      get;
      set;
    }
  }
}
