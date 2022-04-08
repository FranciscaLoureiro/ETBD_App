namespace ETBD.Pages.MyMeals;

[Authorize]
public class DeleteFoodFromMealModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public FoodMeal FoodMeal { get; set; }

    public DeleteFoodFromMealModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
        
    }
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        FoodMeal = _context.FoodMeals.FirstOrDefault(f => f.Id == id);

        if(FoodMeal != null)
        {
            _context.FoodMeals.Remove(FoodMeal);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage("./Edit", new { id = FoodMeal.MealId });
    }
}
