namespace ETBD.Pages.MyMeals;

[BindProperties]
public class CreateModel : PageModel
{

    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public CreateModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void OnGet()
    {
    }

    public Meal Meal { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        UserId = _userManager.GetUserId(User);
        Meal.UserId = UserId;

        _context.Meals.Add(Meal);
        await _context.SaveChangesAsync();

        return RedirectToPage("./AddFoodToMeal", new { MealId = Meal.Id});
    }
}
