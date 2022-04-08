namespace ETBD.Pages.MyFasting;

[Authorize]
[BindProperties]
public class IndexModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public string UserId { get; set; }
    public List<Meal> Meals { get; set; }
    public List<DateTime> Days { get; set; }
    public List<FastingDay> FastingDays { get; set; }

    public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void OnGet()
    {
        UserId = _userManager.GetUserId(User);

        Meals = _context.Meals
            .Where(m => m.UserId == UserId)
            .OrderBy(m => m.Date)
            .ToList();

        Days = new List<DateTime>();
        FastingDays = new List<FastingDay>();

        foreach (var meal in Meals)
        {
            Days.Add(meal.Date.Date);
        }

        Days = Days.Distinct().ToList();


        for (var i = 0; i < Days.Count; i++)
        {
            var date = Days[i];

            List<Meal> TodayMeals = Meals
                .Where(m => m.Date >= date && m.Date < date.AddDays(1))
                .OrderBy(m => m.Date)
                .ToList();

            FastingDay FastingDay = new FastingDay();
            FastingDay.Date = date;
            FastingDay.Meals = TodayMeals;
            if (i < Days.Count - 1)
            {
                var nextDate = Days[i + 1];

                DateTime LastMealOfTheDayDate = TodayMeals.Last().Date;
                DateTime FirstMealOfNextDayDate = Meals
                    .Where(m => m.Date >= nextDate && m.Date < nextDate.AddDays(1))
                    .OrderBy(m => m.Date)
                    .ToList()
                    .First()
                    .Date;

                TimeSpan span = (FirstMealOfNextDayDate - LastMealOfTheDayDate);

                FastingDay.FastingPeriod = String.Format("{0,3}h and {1} minutes", span.Days * 24 + span.Hours, span.Minutes);
            }
            else
            {
                FastingDay.FastingPeriod = null;
            }

            FastingDays.Add(FastingDay);
        }
    }
}
