using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;

namespace avatar2.Pages;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpclient;
    private readonly IConfiguration _config;
    private readonly string _openAiEndpoint = "https://api.openai.com/v1/images/generations";
    private readonly Services _services;
    private readonly DigitalOcean _do;
    private readonly Db _db;

    public IndexModel(Db db, IHttpClientFactory client, IConfiguration configuration, Services services, DigitalOcean digitalOcean)
    {
        _httpclient = client;
        _config = configuration;
        _services = services;
        _config = configuration;
        _do = digitalOcean;
        _db = db;
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
                // generate the prompt
                var prompt = _services.GetPrompt(BaseAvatarSelected, AdjectiveSelected, Gender, FinishSelected);
                if (prompt == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "Invalid prompt. Please try again.";
                    return Page();
                }

                // generate the image using OpenAI
                var imageStream = await _services.GenerateOpenAIImage(_config["open_ai_key"], prompt, _httpclient, _openAiEndpoint);
                if (imageStream == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "Error while generating image";
                    return Page();
                }

                // upload the image to digital ocean spaces
                var imageUrl = await _do.UploadImage(imageStream);

                // save details to the database
                await _db.SaveToDb(prompt, imageUrl);

                TempData["success"] = true;
                TempData["msg"] = "Avatar successfully generated!";
                TempData["query"] = prompt;
                TempData["imgUrl"] = imageUrl;

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
