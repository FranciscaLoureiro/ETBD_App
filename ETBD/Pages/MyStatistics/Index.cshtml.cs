namespace ETBD.Pages.MyStatistics;

public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }
    public List<Meal> Meals { get; set; }
    public List<FrameworkDay> FrameworkDays { get; set; }

    public IndexModel(ETBDApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void OnGet()
    {
        UserId = _userManager.GetUserId(User);
        Meals = _context.Meals
            .Where(m => m.UserId == UserId)
            .ToList();

        FrameworkDays = new List<FrameworkDay>();

        foreach (var meal in Meals)
        {
            var date = meal.Date;

            var dayObject = FrameworkDays.Where(d => d.Date.Date >= date.Date && d.Date.Date < date.Date.AddDays(1)).ToList();

            if (dayObject.Count == 0)
            {
                FrameworkDay Day = new FrameworkDay();

                List<Food> Foods = GetTakenFoods(date);
                List<Action> ActionsTaken = GetTakenActions(Foods);

                List<Action> Actions = _context.Actions.ToList();

                List<Action> ActionsNotTaken = GetNotTakenActions(ActionsTaken, Actions);

                Day.Date = date;
                Day.Foods = Foods;
                Day.ActionsTaken = ActionsTaken;
                Day.ActionsNotTaken = ActionsNotTaken;
                Day.Success = Actions.Count() == ActionsTaken.Count();

                FrameworkDays.Add(Day);
            }
        }
    }

    public List<Food> GetTakenFoods(DateTime date)
    {
        List<Meal> DayMeals = Meals.Where(m => m.Date.Date >= date.Date && m.Date < date.Date.AddDays(1)).ToList();
        List<Food> Foods = new List<Food>();

        foreach (var meal in DayMeals)
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

        return Foods.Distinct().ToList();
    }

    public List<Action> GetTakenActions(List<Food> Foods)
    {
        List<Action> ActionsTaken = new List<Action>();
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
        return ActionsTaken.Distinct().ToList();
    }

    public List<Action> GetNotTakenActions(List<Action> ActionsTaken, List<Action> Actions)
    {
        List<Action> ActionsToTake = new List<Action>();

        foreach (var action in Actions)
        {
            if (!ActionsTaken.Contains(action))
            {
                ActionsToTake.Add(action);
            }
        }

        return ActionsToTake.Distinct().ToList();
    }
}
