namespace ETBD.Pages.MyFoods;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;

    public DetailsModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public Food Food { get; set; }

    public List<Action> ActionsList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Food = await _context.Foods
            .Include(f => f.Category).FirstOrDefaultAsync(m => m.Id == id);

        List<ActionFood> ActionFoodList = await _context.ActionFoods
                .Where(x => x.FoodId == Food.Id)
                .Include(x => x.Action)
                .ToListAsync();

        ActionsList = ActionFoodList.Select(x => x.Action).ToList();

        if (Food == null)
        {
            return NotFound();
        }
        return Page();
    }
}
