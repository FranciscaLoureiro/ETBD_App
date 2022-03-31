namespace ETBD.Pages.MyFoods
{
    public class DetailsModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public DetailsModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Foods
                .Include(f => f.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Food == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
