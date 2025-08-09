using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Services
{
    public string? ValidatePrompt(string baseAvatar, string adjective, string aesthetic, string[] validBaseAvatars, string[] validAdjectives, string[] validFinishes)
    {
        if (!validBaseAvatars.Contains(baseAvatar) || !validAdjectives.Contains(adjective) || !validFinishes.Contains(aesthetic))
        {
            return null;
        }

        return $"Genearate a {aesthetic} picture of a {adjective} {baseAvatar}.";
    }

    public async Task<Stream?> GenerateOpenAIImage(string openAiKey, string imagePrompt, IHttpClientFactory httpClient, string openAIEndpoint)
    {
        try
        {
            var http = httpClient.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, openAIEndpoint);
            request.Headers.Add("Authorization", $"Bearer {openAiKey}");
            request.Content = new StringContent(JsonSerializer.Serialize(new OpenAIPostPrompt
            {
                ImagePrompt = imagePrompt,
            }), Encoding.UTF8, "application/json");

            var res = await http.SendAsync(request);

            if (res.StatusCode != HttpStatusCode.OK) return null;

            var data = JsonSerializer.Deserialize<ImageResponse>(await res.Content.ReadAsStringAsync());
            if (data == null) return null;

            var imageUrl = data.Data[0].ImageUrl;
            using (var imageReponse = await http.GetAsync(imageUrl))
            {
                if (imageReponse == null || imageReponse.StatusCode != HttpStatusCode.OK) return null;

                var memoryStream = new MemoryStream();
                var imageStream = await imageReponse.Content.ReadAsStreamAsync();
                if (imageStream == null) return null;

                await imageStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}

public class OpenAIPostPrompt
{
    [JsonPropertyName("prompt")]
    public string ImagePrompt { get; set; }
    [JsonPropertyName("n")]
    public int NumOfImages { get; set; } = 1;
    [JsonPropertyName("size")]
    public string Size { get; set; } = "512x512";
}

public class ImageResponse
{
    [JsonPropertyName("created")]
    public long Created { get; set; }
    [JsonPropertyName("data")]
    public List<ImageData> Data { get; set; }
}

public class ImageData
{
    [JsonPropertyName("url")]
    public string ImageUrl { get; set; }
}