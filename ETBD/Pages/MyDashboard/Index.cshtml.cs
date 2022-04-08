namespace ETBD.Pages.MyDashboard;

[Authorize]
public class IndexModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.IsInRole("Admin"))
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }
}
