namespace ETBD.Pages.MyMeals;

[Authorize]
[BindProperties]
public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }

    public List<Meal> Meals { get; set; }
    public List<MealFoods> MealFoodsList { get; set; }
    public List<FoodMeal> FoodMeals { get; set; }

    public IndexModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void OnGet()
    {
        UserId = _userManager.GetUserId(User);
        Meals = _context.Meals.Where(x => x.UserId == UserId).ToList();

        MealFoodsList = new List<MealFoods>();

        foreach (var meal in Meals)
        {
            List<FoodMeal> FoodMeals = _context.FoodMeals
                .Where(x => x.MealId == meal.Id)
                .Include(x => x.Food)
                .ToList();
            MealFoods MealFood = new()
            {
                MealId = meal.Id,
                Foods = FoodMeals.Select(x => x.Food).ToList(),
            };

            MealFoodsList.Add(MealFood);
        }
    }
}
