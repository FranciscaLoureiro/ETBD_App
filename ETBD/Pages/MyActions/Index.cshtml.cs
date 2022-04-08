namespace ETBD.Pages.MyActions;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public IndexModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Action> Action { get;set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.IsInRole("Admin"))
        {
            return RedirectToPage("/Index");
        }

        Action = await _context.Actions.ToListAsync();

        return Page();
    }
}
