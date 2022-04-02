namespace ETBD.Pages.MyFavouriteList;

public class DeleteModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DeleteModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public FavouriteList FavouriteList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        FavouriteList = await _context.FavouriteLists
            .Include(f => f.Food)
            .Include(f => f.User).FirstOrDefaultAsync(m => m.Id == id);

        if (FavouriteList == null)
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

        FavouriteList = await _context.FavouriteLists.FindAsync(id);

        if (FavouriteList != null)
        {
            _context.FavouriteLists.Remove(FavouriteList);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
