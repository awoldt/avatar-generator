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
    public string BaseAvatarSelected { get; set; }
    public string? Gender { get; set; } //will only show up if user selected human as base
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
}