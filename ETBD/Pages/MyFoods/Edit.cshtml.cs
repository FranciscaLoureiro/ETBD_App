namespace ETBD.Pages.MyFoods;

[Authorize]
public class EditModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public EditModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Food Food { get; set; }
    public List<Action> Actions { get; set; }
    public List<int> SelectedActionsIds { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Food = await _context.Foods
            .Include(f => f.Category).FirstOrDefaultAsync(m => m.Id == id);

        Actions = _context.Actions.ToList();

        List<ActionFood> ActionFoodList = await _context.ActionFoods
                .Where(x => x.FoodId == Food.Id)
                .Include(x => x.Action)
                .ToListAsync();

        SelectedActionsIds = ActionFoodList.Select(x => x.Action.Id).ToList();

        if (Food == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] NewActionsList)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Food).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FoodExists(Food.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Delete
        _context.ActionFoods.RemoveRange(_context.ActionFoods.Where(x => x.FoodId == Food.Id));
        _context.SaveChanges();

        // Add new rows
        foreach (var ActionId in NewActionsList)
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

    private bool FoodExists(int id)
    {
        return _context.Foods.Any(e => e.Id == id);
    }
}
