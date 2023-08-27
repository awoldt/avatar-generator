using Microsoft.AspNetCore.Mvc.Rendering;

public class RaioOptions
{
    public string Value;
    public string Text;
    public string Img;
    public string AltText;
}

public class AvatarForm
{
    public string BaseAvatarSelect { get; set; }
    public string AdjectiveSelected { get; set; }
    public string FinishSelected { get; set; }

    public List<RaioOptions> BaseAvatarOptions = new List<RaioOptions> {
        new RaioOptions {Value = "human", Text = "Human", Img="/imgs/human.svg", AltText="human icon"},
        new RaioOptions {Value = "bird", Text = "Bird", Img="/imgs/bird.svg", AltText="bird icon"},
        new RaioOptions {Value = "cat", Text = "Cat", Img="/imgs/cat.svg", AltText="cat icon"},
        new RaioOptions {Value = "cow", Text = "Cow", Img="/imgs/cow.svg", AltText="cow icon"},
        new RaioOptions {Value = "fish", Text = "Fish", Img="/imgs/fish.svg", AltText="fish icon"}
    };
    public List<SelectListItem> AdjectiveOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "creepy", Text = "Creepy" },
            new SelectListItem { Value = "classy", Text = "Classy" },
            new SelectListItem { Value = "fat", Text = "Fat" },
            new SelectListItem { Value = "grumpy", Text = "Grumpy" },
            new SelectListItem { Value = "muscular", Text = "Muscular" },
            new SelectListItem { Value = "old", Text = "Old" },
            new SelectListItem { Value = "short", Text = "Short" },
            new SelectListItem { Value = "skinny", Text = "Skinny" },
            new SelectListItem { Value = "tall", Text = "Tall" },
            new SelectListItem { Value = "ugly", Text = "Ugly" },
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
        };
}