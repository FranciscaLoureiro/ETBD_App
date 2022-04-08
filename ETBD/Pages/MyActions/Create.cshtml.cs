namespace ETBD.Pages.MyActions;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public CreateModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Action Action { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Actions.Add(Action);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
