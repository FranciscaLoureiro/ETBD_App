namespace ETBD.Pages.MyFavouriteList;

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

    public IList<FavouriteList> FavouriteList { get;set; }

    public async Task OnGetAsync()
    {
        UserId = _userManager.GetUserId(User);
        FavouriteList = await _context.FavouriteLists
            .Where(x => x.UserId == UserId)
            .Include(f => f.Food)
            .Include(f => f.User).ToListAsync();
    }
}
