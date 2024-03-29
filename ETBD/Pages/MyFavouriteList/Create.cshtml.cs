﻿namespace ETBD.Pages.MyFavouriteList;

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
    public FavouriteList FavouriteList { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        UserId = _userManager.GetUserId(User);
        FavouriteList.UserId = UserId;
        _context.FavouriteLists.Add(FavouriteList);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
