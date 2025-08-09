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
        new RaioOptions { Value = "alien", Text = "Alien", Img = "/imgs/alien.svg", AltText = "alien icon" },
        new RaioOptions { Value = "animal", Text = "Animal", Img = "/imgs/animal.svg", AltText = "animal icon" },
        new RaioOptions { Value = "anime", Text = "Anime", Img = "/imgs/anime.svg", AltText = "anime icon" },
        new RaioOptions { Value = "astronaut", Text = "Astronaut", Img = "/imgs/astronaut.svg", AltText = "astronaut icon" },
        new RaioOptions { Value = "bot", Text = "Bot", Img = "/imgs/bot.svg", AltText = "bot icon" },
        new RaioOptions { Value = "cartoon", Text = "Cartoon", Img = "/imgs/cartoon.svg", AltText = "cartoon icon" },
        new RaioOptions { Value = "creature", Text = "Creature", Img = "/imgs/creature.svg", AltText = "creature icon" },
        new RaioOptions { Value = "cyborg", Text = "Cyborg", Img = "/imgs/cyborg.svg", AltText = "cyborg icon" },
        new RaioOptions { Value = "dinosaur", Text = "Dinosaur", Img = "/imgs/dinosaur.svg", AltText = "dinosaur icon" },
        new RaioOptions { Value = "elf", Text = "Elf", Img = "/imgs/elf.svg", AltText = "elf icon" },
        new RaioOptions { Value = "human", Text = "Human", Img = "/imgs/human.svg", AltText = "human icon" },
        new RaioOptions { Value = "monster", Text = "Monster", Img = "/imgs/monster.svg", AltText = "monster icon" },
        new RaioOptions { Value = "ninja", Text = "Ninja", Img = "/imgs/ninja.svg", AltText = "ninja icon" },
        new RaioOptions { Value = "pirate", Text = "Pirate", Img = "/imgs/pirate.svg", AltText = "pirate icon" },
        new RaioOptions { Value = "robot", Text = "Robot", Img = "/imgs/robot.svg", AltText = "robot icon" },
        new RaioOptions { Value = "superhero", Text = "Superhero", Img = "/imgs/superhero.svg", AltText = "superhero icon" },
        new RaioOptions { Value = "vampire", Text = "Vampire", Img = "/imgs/vampire.svg", AltText = "vampire icon" },
        new RaioOptions { Value = "wizard", Text = "Wizard", Img = "/imgs/wizard.svg", AltText = "wizard icon" },
        new RaioOptions { Value = "zombie", Text = "Zombie", Img = "/imgs/zombie.svg", AltText = "zombie icon" }
    };


    public List<SelectListItem> AdjectiveOptions = new List<SelectListItem>
{
    new SelectListItem { Value = "angry", Text = "Angry" },
    new SelectListItem { Value = "beautiful", Text = "Beautiful" },
    new SelectListItem { Value = "brave", Text = "Brave" },
    new SelectListItem { Value = "calm", Text = "Calm" },
    new SelectListItem { Value = "classy", Text = "Classy" },
    new SelectListItem { Value = "colorful", Text = "Colorful" },
    new SelectListItem { Value = "creepy", Text = "Creepy" },
    new SelectListItem { Value = "cute", Text = "Cute" },
    new SelectListItem { Value = "dark", Text = "Dark" },
    new SelectListItem { Value = "dull", Text = "Dull" },
    new SelectListItem { Value = "elegant", Text = "Elegant" },
    new SelectListItem { Value = "fat", Text = "Fat" },
    new SelectListItem { Value = "fierce", Text = "Fierce" },
    new SelectListItem { Value = "funny", Text = "Funny" },
    new SelectListItem { Value = "futuristic", Text = "Futuristic" },
    new SelectListItem { Value = "giant", Text = "Giant" },
    new SelectListItem { Value = "gloomy", Text = "Gloomy" },
    new SelectListItem { Value = "greasy", Text = "Greasy" },
    new SelectListItem { Value = "grumpy", Text = "Grumpy" },
    new SelectListItem { Value = "happy", Text = "Happy" },
    new SelectListItem { Value = "hungry", Text = "Hungry" },
    new SelectListItem { Value = "majestic", Text = "Majestic" },
    new SelectListItem { Value = "mysterious", Text = "Mysterious" },
    new SelectListItem { Value = "muscular", Text = "Muscular" },
    new SelectListItem { Value = "old", Text = "Old" },
    new SelectListItem { Value = "pale", Text = "Pale" },
    new SelectListItem { Value = "playful", Text = "Playful" },
    new SelectListItem { Value = "proud", Text = "Proud" },
    new SelectListItem { Value = "sad", Text = "Sad" },
    new SelectListItem { Value = "scary", Text = "Scary" },
    new SelectListItem { Value = "shiny", Text = "Shiny" },
    new SelectListItem { Value = "short", Text = "Short" },
    new SelectListItem { Value = "skinny", Text = "Skinny" },
    new SelectListItem { Value = "sleepy", Text = "Sleepy" },
    new SelectListItem { Value = "smart", Text = "Smart" },
    new SelectListItem { Value = "spooky", Text = "Spooky" },
    new SelectListItem { Value = "strong", Text = "Strong" },
    new SelectListItem { Value = "tall", Text = "Tall" },
    new SelectListItem { Value = "tiny", Text = "Tiny" },
    new SelectListItem { Value = "ugly", Text = "Ugly" },
    new SelectListItem { Value = "weak", Text = "Weak" },
    new SelectListItem { Value = "weird", Text = "Weird" },
    new SelectListItem { Value = "wild", Text = "Wild" },
    new SelectListItem { Value = "wise", Text = "Wise" },
    new SelectListItem { Value = "young", Text = "Young" }
};


    public List<SelectListItem> FinishOptions = new List<SelectListItem>
{
    new SelectListItem { Value = "abstract", Text = "Abstract" },
    new SelectListItem { Value = "anime", Text = "Anime" },
    new SelectListItem { Value = "cartoon", Text = "Cartoon" },
    new SelectListItem { Value = "celshaded", Text = "Cel Shaded" },
    new SelectListItem { Value = "claymation", Text = "Claymation" },
    new SelectListItem { Value = "comicbook", Text = "Comic Book" },
    new SelectListItem { Value = "digitalpaint", Text = "Digital Paint" },
    new SelectListItem { Value = "gothic", Text = "Gothic" },
    new SelectListItem { Value = "lineart", Text = "Line Art" },
    new SelectListItem { Value = "lowpoly", Text = "Low Poly" },
    new SelectListItem { Value = "minimalism", Text = "Minimalism" },
    new SelectListItem { Value = "neon", Text = "Neon" },
    new SelectListItem { Value = "oilpainting", Text = "Oil Painting" },
    new SelectListItem { Value = "pixelart", Text = "Pixel Art" },
    new SelectListItem { Value = "retro", Text = "Retro" },
    new SelectListItem { Value = "steampunk", Text = "Steampunk" },
    new SelectListItem { Value = "trippy", Text = "Trippy" },
    new SelectListItem { Value = "vaporwave", Text = "Vaporwave" },
    new SelectListItem { Value = "vintage", Text = "Vintage" },
    new SelectListItem { Value = "watercolor", Text = "Watercolor" }
};


    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            string[] validBaseAvatars = BaseAvatarOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();
            string[] validAdjectives = AdjectiveOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();
            string[] validFinishes = FinishOptions.Select(x =>
                {
                    return x.Value;
                }).ToArray();


            try
            {
                var prompt = _services.ValidatePrompt(BaseAvatarSelected, AdjectiveSelected, FinishSelected, validBaseAvatars, validAdjectives, validFinishes);

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
                await _db.SaveToDb(new Avatar { Id = null, ImageUrl = imageUrl, ImgageDetails = new AvatarDetails { Adjective = AdjectiveSelected, Base = BaseAvatarSelected, Aesthetic = FinishSelected } });

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
