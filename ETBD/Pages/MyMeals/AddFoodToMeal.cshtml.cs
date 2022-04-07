namespace ETBD.Pages.MyMeals;

public class AddFoodToMealModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    public SelectList Foods { get; set; }
    [BindProperty]
    public int _MealId { get; set; }
    [BindProperty]
    public FoodMeal FoodMeal { get; set; }
    [BindProperty]
    public int SelectedFoodId { get; set; }
    public Meal Meal { get; set; }
    public Food Food { get; set; }

    public AddFoodToMealModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet(int MealId)
    {
        _MealId = MealId;
        Foods = new SelectList(_context.Foods, "Id", "Name");
    }
    public async Task<IActionResult> OnPostAsync(string mealId)
    {
        Meal = _context.Meals.Where(c => c.Id == int.Parse(mealId)).FirstOrDefault();
        Food = _context.Foods.Where(c => c.Id == SelectedFoodId).FirstOrDefault();

        FoodMeal.Food = Food;
        FoodMeal.Meal = Meal;

        _context.FoodMeals.Add(FoodMeal);
        await _context.SaveChangesAsync();

        return RedirectToAction("OnGet", new { MealId = _MealId });
    }
}
