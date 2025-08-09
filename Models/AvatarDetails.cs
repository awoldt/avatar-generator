using System.Text.Json.Serialization;

public class AvatarDetails
{
  [JsonPropertyName("adjective")]
  public string Adjective { get; set; }

  [JsonPropertyName("base")]
  public string Base { get; set; }

  [JsonPropertyName("aesthetic")]
  public string Aesthetic { get; set; }
}