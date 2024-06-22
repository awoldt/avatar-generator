using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace avatar2.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpClientFactory _httpclient;
    private readonly IConfiguration _config;

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory client, IConfiguration configuration)
    {
        _logger = logger;
        _httpclient = client;
        _config = configuration;
    }

    public string BaseAvatarSelected { get; set; }
    public string? Gender { get; set; } // only sets value if user selected human as base
    public string AdjectiveSelected { get; set; }
    public string FinishSelected { get; set; }

    public List<RaioOptions> BaseAvatarOptions = new List<RaioOptions> {
        new RaioOptions {Value = "human", Text = "Human", Img="/imgs/human.svg", AltText="human icon"},
        new RaioOptions {Value = "animal", Text = "Animal", Img="/imgs/bird.svg", AltText="animal icon"},
    };

    public List<SelectListItem> AdjectiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "beautiful", Text = "Beautiful" },
            new SelectListItem { Value = "brave", Text = "Brave" },
            new SelectListItem { Value = "creepy", Text = "Creepy" },
            new SelectListItem { Value = "classy", Text = "Classy" },
            new SelectListItem { Value = "dull", Text = "Dull" },
            new SelectListItem { Value = "fat", Text = "Fat" },
            new SelectListItem { Value = "greasy", Text = "Greasy" },
            new SelectListItem { Value = "grumpy", Text = "Grumpy" },
            new SelectListItem { Value = "happy", Text = "Happy" },
            new SelectListItem { Value = "muscular", Text = "Muscular" },
            new SelectListItem { Value = "old", Text = "Old" },
            new SelectListItem { Value = "pale", Text = "Pale" },
            new SelectListItem { Value = "shiny", Text = "Shiny" },
            new SelectListItem { Value = "short", Text = "Short" },
            new SelectListItem { Value = "skinny", Text = "Skinny" },
            new SelectListItem { Value = "tall", Text = "Tall" },
            new SelectListItem { Value = "ugly", Text = "Ugly" },
            new SelectListItem { Value = "weak", Text = "Weak" },
            new SelectListItem { Value = "wise", Text = "Wise" },
            new SelectListItem { Value = "young", Text = "Young" },
        };

    public List<SelectListItem> FinishOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "abstract", Text = "Abstract" },
            new SelectListItem { Value = "anime", Text = "Anime" },
            new SelectListItem { Value = "cartoon", Text = "Cartoon" },
            new SelectListItem { Value = "claymation", Text = "Claymation" },
            new SelectListItem { Value = "gothic", Text = "Gothic" },
            new SelectListItem { Value = "minimalism", Text = "Minimalism" },
            new SelectListItem { Value = "trippy", Text = "Trippy" },
            new SelectListItem { Value = "vintage", Text = "Vintage" },
        };

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            try
            {
                const string openAiEndpoint = "https://api.openai.com/v1/images/generations";
                string promptString = "";
                // human avatar
                if (BaseAvatarSelected == "human")
                {
                    promptString = $"A {FinishSelected} picture of a {AdjectiveSelected} {Gender}";
                }
                //animal avatar
                else if (BaseAvatarSelected == "animal")
                {
                    promptString = $"A {FinishSelected} picture of a {AdjectiveSelected} animal";
                }
                // invalid base avatar
                else
                {
                    TempData["success"] = false;
                    TempData["msg"] = "Invalid base avatar";
                    return Page();
                }

                // make a request to OpenAI to generate image
                var http = _httpclient.CreateClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, openAiEndpoint);
                request.Headers.Add("Authorization", $"Bearer {_config["open_ai_key"]}");
                request.Content = new StringContent(JsonSerializer.Serialize(new OpenAIPostPrompt
                {
                    ImagePrompt = promptString,
                }), Encoding.UTF8, "application/json");
                var res = await http.SendAsync(request);

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "There was an error while sending the requset";

                    return Page();
                }

                var data = JsonSerializer.Deserialize<ImageResponse>(await res.Content.ReadAsStringAsync());
                if (data == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "There was an error deserializing response content";

                    return Page();
                }

                TempData["success"] = true;
                TempData["msg"] = "Avatar successfully generated!";
                TempData["query"] = promptString;
                TempData["imgUrl"] = data.Data[0].ImageUrl;

                return Page();
            }
            catch (Exception ex)
            {
                TempData["success"] = false;
                TempData["msg"] = ex.ToString();
                return Page();
            }

        }

        // form is not formatted correctly
        TempData["success"] = false;
        TempData["msg"] = "Error while validating form data";
        return Page();
    }
}

public class RaioOptions
{
    public required string Value;
    public required string Text;
    public required string Img;
    public required string AltText;
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