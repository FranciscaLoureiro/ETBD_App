namespace ETBD.Pages.MyActions;

[Authorize]
public class EditModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public EditModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Action Action { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Action = await _context.Actions.FirstOrDefaultAsync(m => m.Id == id);

        if (Action == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Action).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActionExists(Action.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool ActionExists(int id)
    {
        return _context.Actions.Any(e => e.Id == id);
    }
}
