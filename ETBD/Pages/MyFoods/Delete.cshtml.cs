namespace ETBD.Pages.MyFoods;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DeleteModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Food Food { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Food = await _context.Foods
            .Include(f => f.Category).FirstOrDefaultAsync(m => m.Id == id);

        if (Food == null)
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

        Food = await _context.Foods.FindAsync(id);

        if (Food != null)
        {
            _context.Foods.Remove(Food);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
