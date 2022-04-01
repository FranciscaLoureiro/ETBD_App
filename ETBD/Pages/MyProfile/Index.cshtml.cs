namespace ETBD.Pages.MyProfile
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public string UserId { get; set; }
        public Profile MyProfile { get; set; }

        public IndexModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            UserId = _userManager.GetUserId(User);
            MyProfile = _context.Profiles.FirstOrDefault(m => m.UserId == UserId);

            if (MyProfile == null)
            {
                return RedirectToPage("Create");
            }

            return Page();
        }
    }
}
