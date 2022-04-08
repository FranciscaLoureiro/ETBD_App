namespace ETBD.Pages.MyBlackList;

[Authorize]
public class CreateModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult OnGet()
    {
        ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Name");
        return Page();
    }
  
    [BindProperty]
    public BlackList BlackList { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        UserId = _userManager.GetUserId(User);
        BlackList.UserId = UserId;
        _context.BlackLists.Add(BlackList);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
