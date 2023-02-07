using Microsoft.AspNetCore.Mvc;

namespace AvatarImageGenerator.Models
{
    public class Avatar
    {
        
        public string base_avatar { get; set; }
        public string adjective { get; set; }
        public string finish { get; set; }
        public string? gender { get; set; }

    }
}
