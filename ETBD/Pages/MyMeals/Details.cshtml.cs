namespace ETBD.Pages.MyMeals;

[Authorize]
[BindProperties]
public class DetailsModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public DetailsModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Meal> Meals { get; set; }
    public List<FoodMeal> FoodMeals { get; set; }
    public Meal Meal { get; set; }

    public void OnGet(int? id)
    {
        
        Meal = _context.Meals
                .FirstOrDefault(m => m.Id == id);
        
        FoodMeals = _context.FoodMeals
                .Where(m => m.MealId == id).Include(m => m.Food).ToList();          
    }
}
