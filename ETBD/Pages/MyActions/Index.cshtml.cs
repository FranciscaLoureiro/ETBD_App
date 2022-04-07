namespace ETBD.Pages.MyActions;

public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public IndexModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Action> Action { get;set; }

    public async Task OnGetAsync()
    {
        Action = await _context.Actions.ToListAsync();
    }
}
