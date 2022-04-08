namespace ETBD.Pages.MyCategories;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DeleteModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Category = await _context.Categories.FindAsync(id);

        if (Category != null)
        {
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
