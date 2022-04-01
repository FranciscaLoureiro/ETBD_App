namespace ETBD.Pages.MyFoods
{
    public class IndexModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public IndexModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get; set; }
        [BindProperty]
        public List<FoodActions> FoodActionsList { get; set; }

        public async Task OnGetAsync()
        {
            Food = await _context.Foods
                .Include(f => f.Category)
                .ToListAsync();

            FoodActionsList = new List<FoodActions>();

            foreach (var food in Food)
            {
                List<ActionFood> ActionFoodList = await _context.ActionFoods
                    .Where(x => x.FoodId == food.Id)
                    .Include(x => x.Action)
                    .ToListAsync();

                FoodActions FoodActionsItem = new()
                {
                    FoodId = food.Id,
                    Actions = ActionFoodList.Select(x => x.Action).ToList()

                };

                FoodActionsList.Add(FoodActionsItem);
            }
        }
    }
}
