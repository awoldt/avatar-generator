using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class GalleryBasePageModel : PageModel
{

  private Db _db { get; set; }

  public GalleryBasePageModel(Db db)
  {
    _db = db;
  }

  public Avatar[]? Avatars { get; set; }
  public string[] BaseAvatars = Constants.BaseAvatarOptions.Select(x => x.Text).ToArray();
  public string? BaseFilter { get; set; }

  public async Task<IActionResult> OnGetAsync(string baseAvatar)
  {

    BaseFilter = baseAvatar;
    Avatars = await _db.GetGalleryImages(baseAvatar);
    return Page();
  }
}