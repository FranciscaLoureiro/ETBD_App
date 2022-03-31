namespace ETBD.Pages.MyFoods
{
    public class IndexModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public IndexModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; }

        public async Task OnGetAsync()
        {
            Food = await _context.Foods
                .Include(f => f.Category).ToListAsync();
        }
    }
}
