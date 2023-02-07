using AvatarImageGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AvatarImageGenerator.Controllers
{
    public class HomeController : Controller
    {
        private static HttpClient _httpClient = new HttpClient();
        private const string baseUrl = "https://api.openai.com/v1/images/generations";
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration Config)
        {
            _configuration = Config;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(Avatar avatarDetails)
        {
            try
            {   
                string apiKey = _configuration["OpenAI_API_Key"];
                _httpClient.DefaultRequestHeaders.Remove("Authorization"); //allows to set new header
                _httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //generate prompt to send to openai based on users avatar details
                //ex: a painting of a fat man
                string PROMPT;
                if (avatarDetails.gender != null)
                {
                    string g = avatarDetails.gender == "guy" ? "man" : "woman";
                    PROMPT = "A " + avatarDetails.finish + " picture of a " + avatarDetails.adjective + " " + g;
                }
                else
                {
                    PROMPT = "A " + avatarDetails.finish + " picture of a " + avatarDetails.adjective + " " + avatarDetails.base_avatar;
                }

                ImageGeneration newImage = new ImageGeneration();
                if(newImage.size != "256x256") {
                    TempData["500"] = "There was an error while generating the image. Try again.";
                    TempData["Error"] = "The image that being generated was too large.";
                    return RedirectToAction("Index");
                }
                newImage.prompt = PROMPT;

                var req = await _httpClient.PostAsJsonAsync(baseUrl, newImage);
                var res = await req.Content.ReadAsStringAsync();

                var responseData = JsonSerializer.Deserialize<ImageResponse>(res);
                if(responseData != null && responseData.data.Count != 0)
                {
                    TempData["200"] = responseData.data[0].url;
                    TempData["ImageQuery"] = PROMPT;
                    return RedirectToAction("Index");
                }
                else {
                    TempData["500"] = "There was an error while generating the image. Try again.";
                    TempData["Error"] = "The query supplied could not generate a result. Try changing the prompts.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["500"] = "There was an error while generating the image. Try again.";
                TempData["Error"] = ex.ToString();
                return RedirectToAction("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ImageResponse
    {
        public int created { get; set; }
        public List<ImageUrls> data { get; set; }
    }
    public class ImageUrls
    {
        public string url { get; set; }
    }
}