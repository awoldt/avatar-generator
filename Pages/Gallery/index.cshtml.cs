using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class GalleryPageModel : PageModel
{
  public GalleryPageModel(Db db)
  {
    _db = db;
  }

  private Db _db { get; set; }

  public Avatar[]? Avatars { get; set; }

  public string[] BaseAvatars = Constants.BaseAvatarOptions.Select(x => x.Text).ToArray();

  public async Task<IActionResult> OnGetAsync()
  {
    Avatars = await _db.GetGalleryImages(null);
    return Page();
  }
}