namespace ETBD.Pages.MyFoods
{
    public class CreateModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public CreateModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Categories = new SelectList(_context.Categories, "Id", "Name");
            Actions = _context.Actions.ToList();
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        public SelectList Categories { get; set; }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        public List<Action> Actions { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] ActionsList)
        {
            Food.Category = _context.Categories.Where(c => c.Id == SelectedCategoryId).FirstOrDefault();
            ModelState.Clear();
            TryValidateModel(Food);
            TryValidateModel(Food.Category);

            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            _context.Foods.Add(Food);
            await _context.SaveChangesAsync();

            foreach (var ActionId in ActionsList)
            {
                Action Action = _context.Actions.FirstOrDefault(a => a.Id == int.Parse(ActionId));
                if (Action != null)
                {
                    ActionFood ActionFood = new ActionFood
                    {
                        Food = Food,
                        Action = Action
                    };

                    TryValidateModel(ActionFood);

                    _context.ActionFoods.Add(ActionFood);

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
