using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using avatargeneratorV2.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace avatargeneratorV2.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpclient;
    private readonly IConfiguration _config;

    public HomeController(IHttpClientFactory client, IConfiguration config)
    {
        _httpclient = client;
        _config = config;
    }

    public IActionResult Index()
    {
        return View(new AvatarForm { });
    }

    [HttpPost]
    public async Task<IActionResult> Index(AvatarForm form)
    {
        if (ModelState.IsValid)
        {
            try
            {
                HttpClient c = _httpclient.CreateClient();
                c.BaseAddress = new Uri("https://api.openai.com/v1/images/generations");
                c.DefaultRequestHeaders.Add("Authorization", "Bearer " + _config["open_ai_api_key"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage? res = null;

                //HUMAN BASE AVATAR
                if (form.BaseAvatarSelected == "human")
                {
                    var content = new StringContent(JsonSerializer.Serialize(new ImageBodyFormat { prompt = $"A {form.FinishSelected} picture of a {form.AdjectiveSelected} {form.Gender}", n = 1, size = "256x256" }), Encoding.UTF8, "application/json");
                    res = await c.PostAsync(c.BaseAddress.ToString(), content);
                }
                //ANIMAL BASE AVATAR
                else if (form.BaseAvatarSelected == "animal")
                {
                    var content = new StringContent(JsonSerializer.Serialize(new ImageBodyFormat { prompt = $"A {form.FinishSelected} picture of a random {form.AdjectiveSelected} animal", n = 1, size = "256x256" }), Encoding.UTF8, "application/json");
                    res = await c.PostAsync(c.BaseAddress.ToString(), content);
                }
                //ERROR, NO BASE AVATAR VALUE IN FORM
                else
                {
                    TempData["success"] = false;
                    TempData["msg"] = "There was an error generating the base avatar";
                    return View();
                }

                if (res == null)
                {
                    TempData["success"] = false;
                    TempData["msg"] = "There was an error while generating avatar";
                    return View();
                }

                if (res.IsSuccessStatusCode)
                {
                    TempData["success"] = true;
                    TempData["msg"] = "Image successfully generated!";
                    TempData["query"] = form.BaseAvatarSelected == "human" ? $"A {form.AdjectiveSelected} {form.Gender}, with the picuture having a {form.FinishSelected} aesthetic" : $"A random {form.AdjectiveSelected} animal, with the picuture having a {form.FinishSelected} aesthetic";

                    string resBody = await res.Content.ReadAsStringAsync();
                    OpenAiImageResponse imgData = JsonSerializer.Deserialize<OpenAiImageResponse>(resBody)!;

                    if (imgData.data != null && imgData.data.Count > 0)
                    {
                        TempData["url"] = imgData.data[0].url;
                    }


                    return View();
                }
                TempData["success"] = false;
                TempData["msg"] = "There was an error while making a request to OpenAI";
                return View();

            }
            catch (Exception ex)
            {
                TempData["success"] = false;
                TempData["msg"] = ex.ToString();
                return View();
            }

        }

        //form is not formatted correctly
        TempData["success"] = false;
        TempData["msg"] = "Error while accessing form data";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public class ImageBodyFormat
{
    public string prompt { get; set; }
    public int n;
    public string size;
}

public class ImgUrls
{
    public string url { get; set; }
}

public class OpenAiImageResponse
{
    public int created { get; set; }
    public List<ImgUrls> data { get; set; }

}