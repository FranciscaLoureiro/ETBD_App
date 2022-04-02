namespace ETBD.Pages.MyBlackList;

public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<BlackList> BlackList { get;set; }

    public async Task OnGetAsync()
    {
        UserId = _userManager.GetUserId(User);
        BlackList = await _context.BlackLists
            .Where(x => x.UserId == UserId)
            .Include(b => b.Food)
            .Include(b => b.User).ToListAsync();
    }
}
