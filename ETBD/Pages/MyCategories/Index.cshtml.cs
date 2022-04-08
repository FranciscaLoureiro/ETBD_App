namespace ETBD.Pages.MyCategories;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public IndexModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Category> Category { get;set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!User.IsInRole("Admin"))
        {
            return RedirectToPage("/Index");
        }

        Category = await _context.Categories.ToListAsync();

        return Page();
    }
}
