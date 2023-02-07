using System.ComponentModel.DataAnnotations;

namespace AvatarImageGenerator.Models
{
	public class ImageGeneration
	{
		[Required]
		public string prompt { get; set; }
		public int n { get; set; } = 1; //can only generate 1 image per request
		public string size { get; set; } = "256x256"; //most cost effective size
	}
}
