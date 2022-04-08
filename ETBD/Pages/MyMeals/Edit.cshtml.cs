namespace ETBD.Pages.MyMeals;

[Authorize]
[BindProperties]
public class EditModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public EditModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Meal Meal { get; set; }
    public List<FoodMeal> FoodMeals { get; set; }

    public void OnGet(int? id)
    {

        Meal = _context.Meals
                .FirstOrDefault(m => m.Id == id);

        FoodMeals = _context.FoodMeals
                .Where(m => m.MealId == id)
                .Include(m => m.Food)
                .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Attach(Meal).State = EntityState.Modified;

        return RedirectToPage("./AddFoodToMeal", new {MealId =Meal.Id});
    }

    private bool MealExists(int id)
    {
        return _context.Meals.Any(e => e.Id == id);
    }
}
