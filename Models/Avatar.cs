using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;

public class Avatar
{
  [JsonPropertyName("id")]
  public int? Id { get; set; }

  [JsonPropertyName("created_at")]
  public DateTime CreatedAt { get; set; }

  [JsonPropertyName("image_url")]
  public string ImageUrl { get; set; }

  [JsonPropertyName("image_details")]
  public AvatarDetails ImgageDetails { get; set; }
}