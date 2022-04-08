namespace ETBD.Pages.MyCategories;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DetailsModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public Category Category { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

        if (Category == null)
        {
            return NotFound();
        }
        return Page();
    }
}
