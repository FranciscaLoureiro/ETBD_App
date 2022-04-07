namespace ETBD.Pages.MyMeals;

public class DeleteModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DeleteModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Meal Meal { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Meal = await _context.Meals
            .Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);

        if (Meal == null)
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

        Meal = await _context.Meals.FindAsync(id);

        if (Meal != null)
        {
            _context.Meals.Remove(Meal);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
