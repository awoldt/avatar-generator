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
    public string AestheticSelected { get; set; }

    public List<RadioOptions> BaseAvatarRadioList = Constants.BaseAvatarOptions;
    public List<SelectListItem> AdjectivesSelectList = Constants.AdjectiveOptions;
    public List<SelectListItem> AestheticsSelectList = Constants.AesthecticOptions;

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            string[] validBaseAvatars = Constants.BaseAvatarOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();
            string[] validAdjectives = Constants.AdjectiveOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();
            string[] validAesthetics = Constants.AesthecticOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();


            try
            {
                var prompt = _services.ValidatePrompt(BaseAvatarSelected, AdjectiveSelected, AestheticSelected, validBaseAvatars, validAdjectives, validAesthetics);

                if (prompt == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "Invalid prompt. Please try again.";
                    return Page();
                }

                var imageStream = await _services.GenerateOpenAIImage(_config["open_ai_key"]!, prompt, _httpclient, _openAiEndpoint);
                if (imageStream == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "Error while generating image";
                    return Page();
                }

                var imageUrl = await _do.UploadImage(imageStream);
                await _db.SaveToDb(new Avatar { Id = null, ImageUrl = imageUrl, ImgageDetails = new AvatarDetails { Adjective = AdjectiveSelected, Base = BaseAvatarSelected, Aesthetic = AestheticSelected } });

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