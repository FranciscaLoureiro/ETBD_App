namespace ETBD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public ActionResult OnGet()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("MyProfile/Index");
            }

            return Page();
        }
    }
}