namespace ETBD.Pages.MyDashboard;

[Authorize]
[BindProperties]
public class Top10FoodsModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    public List<FoodCount> FoodCounts { get; set; }
    public List<FoodCount> FinalFoodCounts { get; set; }
    public List<FoodMeal> FoodMeals { get; set; }
    public Top10FoodsModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
        FoodMeals = _context.FoodMeals
            .Include(f => f.Food)
            .ToList();

        FoodCounts = new List<FoodCount>();

        foreach (var foodMeal in FoodMeals)
        {
            var foodCountObject = FoodCounts.Where(f => f.Food.Id == foodMeal.FoodId);

            if (foodCountObject.Count() == 0)
            {
                List<FoodMeal> TotalFoodMeals = FoodMeals.Where(f => f.FoodId == foodMeal.FoodId).ToList();
                FoodCount FoodCount = new();
                FoodCount.Count = TotalFoodMeals.Count();
                FoodCount.Food = foodMeal.Food;

                FoodCounts.Add(FoodCount);
            }
        }
        FoodCounts.Sort((x, y) => y.Count.CompareTo(x.Count));
        FinalFoodCounts = FoodCounts.Take(10).ToList();
    }
}
