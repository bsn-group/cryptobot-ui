using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("exchange", Schema = "public")]
  public partial class Exchange
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

    public IEnumerable<FuturesSignal> FuturesSignals { get; set; }
    [ConcurrencyCheck]
    public bool active
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string code
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
  }
}
