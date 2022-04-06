namespace ETBD.Pages.MySuggestions;

[BindProperties]
public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public List<Meal> Meals { get; set; }
    public List<Action> ActionsTaken { get; set; }
    public List<Action> ActionsToTake { get; set; }
    public List<Action> Actions { get; set; }
    public List<Food> Foods { get; set; }
    public List<Food> SuggestedFoods { get; set; }
    public string UserId { get; set; }
    public int NumberOfMeals { get; set; }

    public IndexModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void OnGet()
    {
        var date = DateTime.Now.Date;
        UserId = _userManager.GetUserId(User);
        Meals = _context.Meals
            .Where(m => m.Date.Date >= date && m.Date.Date < date.AddDays(1) && UserId == m.UserId)
            .ToList();

        NumberOfMeals = Meals.Count();

        Foods = new List<Food>();
        foreach (var meal in Meals)
        {
            List<FoodMeal> FoodMeals = _context.FoodMeals
                .Where(f => f.MealId == meal.Id)
                .Include(f => f.Food)
                .ToList();

            foreach (var foodMeal in FoodMeals)
            {
                Foods.Add(foodMeal.Food);
            }
        }

        ActionsTaken = new List<Action>();

        foreach (var food in Foods)
        {
            List<ActionFood> ActionFoods = _context.ActionFoods
                .Where(f => f.FoodId == food.Id)
                .Include(f => f.Action)
                .ToList();

            foreach (var actionFood in ActionFoods)
            {
                ActionsTaken.Add(actionFood.Action);
            }
        }

        Actions = _context.Actions.ToList();
        ActionsToTake = new List<Action>();

        foreach (var action in Actions)
        {
            if (!ActionsTaken.Contains(action))
            {
                ActionsToTake.Add(action);
            }
        }

        ActionsTaken = ActionsTaken.Distinct().ToList();
        Foods = Foods.Distinct().ToList();
        ActionsToTake = ActionsToTake.Distinct().ToList();

        SuggestedFoods = new List<Food>();

        foreach (var action in ActionsToTake)
        {
            List<ActionFood> ActionFoods = _context.ActionFoods
                .Where(f => f.ActionId == action.Id)
                .Include(f => f.Food)
                .ToList();

            if (ActionFoods.Count() > 0)
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(0, ActionFoods.Count());

                SuggestedFoods.Add(ActionFoods[randomIndex].Food);
            }
        }
    }
}
