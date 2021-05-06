using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptobotUi.Models.Cryptodb
{
  [Table("configs", Schema = "public")]
  public partial class Config
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
    public string name
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string json_value
    {
      get;
      set;
    }
  }
}
