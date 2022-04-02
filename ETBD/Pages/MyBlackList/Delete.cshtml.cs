namespace ETBD.Pages.MyBlackList;

public class DeleteModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DeleteModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public BlackList BlackList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        BlackList = await _context.BlackLists
            .Include(b => b.Food)
            .Include(b => b.User).FirstOrDefaultAsync(m => m.Id == id);

        if (BlackList == null)
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

        BlackList = await _context.BlackLists.FindAsync(id);

        if (BlackList != null)
        {
            _context.BlackLists.Remove(BlackList);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
