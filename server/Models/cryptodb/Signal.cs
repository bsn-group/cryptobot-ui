using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("signal", Schema = "public")]
  public partial class Signal
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
    public Int64 signal_id
    {
      get;
      set;
    }

    public IEnumerable<SignalCommand> SignalCommands { get; set; }
    [ConcurrencyCheck]
    public DateTime created_date_time
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? updated_date_time
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
    public Exchange Exchange { get; set; }
    [ConcurrencyCheck]
    public Int64 strategy_pair_id
    {
      get;
      set;
    }
    public Strategy Strategy { get; set; }
    [ConcurrencyCheck]
    public string symbol
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
    public string position_type
    {
      get;
      set;
    }
  }
}
