namespace ETBD.Pages.MyDashboard;

public class PopulateDatabaseModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    List<Category> Categories { get; set; }

    public PopulateDatabaseModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
    }

    public void OnPost()
    {
        // Categories
        string categoriesPath = @"Csv\Categories.csv";
        using (var reader = new StreamReader(categoriesPath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                Category Category = new Category();

                Category.Name = values[1];

                _context.Categories.Add(Category);
            }

            _context.SaveChanges();
        }

        // Actions
        string actionsPath = @"Csv\Actions.csv";
        using (var reader = new StreamReader(actionsPath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                Action Action = new Action();

                Action.Name = values[1];

                _context.Actions.Add(Action);
            }

            _context.SaveChanges();
        }

        // Foods
        string foodsPath = @"Csv\Foods.csv";
        using (var reader = new StreamReader(foodsPath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                Food Food = new Food();
                Category Category = _context.Categories.FirstOrDefault(c => c.Id == int.Parse(values[2]));

                Food.Name = values[1];
                Food.Category = Category;

                _context.Foods.Add(Food);
            }

            _context.SaveChanges();
        }

        // FoodActions
        string foodActionsPath = @"Csv\FoodActions.csv";
        using (var reader = new StreamReader(foodActionsPath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                ActionFood ActionFood = new ActionFood();

                Food Food = _context.Foods.FirstOrDefault(c => c.Id == int.Parse(values[0]));
                Action Action = _context.Actions.FirstOrDefault(c => c.Id == int.Parse(values[1]));

                ActionFood.Food = Food;
                ActionFood.Action = Action;

                _context.ActionFoods.Add(ActionFood);
            }

            _context.SaveChanges();
        }
    }
}
