using avatar2.Pages;
using Microsoft.AspNetCore.Mvc.Rendering;

public class Constants
{
  public static List<RadioOptions> BaseAvatarOptions = [
        new RadioOptions { Value = "alien", Text = "Alien", Img = "/imgs/alien.svg", AltText = "alien icon" },
        new RadioOptions { Value = "animal", Text = "Animal", Img = "/imgs/animal.svg", AltText = "animal icon" },
        new RadioOptions { Value = "anime", Text = "Anime", Img = "/imgs/anime.svg", AltText = "anime icon" },
        new RadioOptions { Value = "astronaut", Text = "Astronaut", Img = "/imgs/astronaut.svg", AltText = "astronaut icon" },
        new RadioOptions { Value = "bot", Text = "Bot", Img = "/imgs/bot.svg", AltText = "bot icon" },
        new RadioOptions { Value = "cartoon", Text = "Cartoon", Img = "/imgs/cartoon.svg", AltText = "cartoon icon" },
        new RadioOptions { Value = "creature", Text = "Creature", Img = "/imgs/creature.svg", AltText = "creature icon" },
        new RadioOptions { Value = "cyborg", Text = "Cyborg", Img = "/imgs/cyborg.svg", AltText = "cyborg icon" },
        new RadioOptions { Value = "dinosaur", Text = "Dinosaur", Img = "/imgs/dinosaur.svg", AltText = "dinosaur icon" },
        new RadioOptions { Value = "elf", Text = "Elf", Img = "/imgs/elf.svg", AltText = "elf icon" },
        new RadioOptions { Value = "human", Text = "Human", Img = "/imgs/human.svg", AltText = "human icon" },
        new RadioOptions { Value = "monster", Text = "Monster", Img = "/imgs/monster.svg", AltText = "monster icon" },
        new RadioOptions { Value = "ninja", Text = "Ninja", Img = "/imgs/ninja.svg", AltText = "ninja icon" },
        new RadioOptions { Value = "pirate", Text = "Pirate", Img = "/imgs/pirate.svg", AltText = "pirate icon" },
        new RadioOptions { Value = "robot", Text = "Robot", Img = "/imgs/robot.svg", AltText = "robot icon" },
        new RadioOptions { Value = "superhero", Text = "Superhero", Img = "/imgs/superhero.svg", AltText = "superhero icon" },
        new RadioOptions { Value = "vampire", Text = "Vampire", Img = "/imgs/vampire.svg", AltText = "vampire icon" },
        new RadioOptions { Value = "wizard", Text = "Wizard", Img = "/imgs/wizard.svg", AltText = "wizard icon" },
        new RadioOptions { Value = "zombie", Text = "Zombie", Img = "/imgs/zombie.svg", AltText = "zombie icon" }
    ];

  public static List<SelectListItem> AdjectiveOptions =
[
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
];


  public static List<SelectListItem> AesthecticOptions =
[
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
];
}