namespace ETBD.Pages.MyDashboard;

[Authorize]
public class UsersModel : PageModel
{
    private readonly ETBDApp.Data.ApplicationDbContext _context;
    public List<IdentityUser> Users { get; set; }
    [BindProperty]
    public List<UserProfile> UserProfiles { get; set; }
    public List<Meal> Meals { get; set; }
    public UsersModel(ETBDApp.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
        Users = _context.Users.ToList();
        UserProfiles = new List<UserProfile>();

        foreach (var user in Users)
        {
            UserProfile UserProfile = new UserProfile();
            List<Meal> Meals = new List<Meal>();

            Profile Profile = _context.Profiles.FirstOrDefault(p => p.UserId == user.Id);
            Console.WriteLine(Profile.FirstName);

            Meals = _context.Meals.Where(m => m.UserId == user.Id).ToList();

            UserProfile.UserId = user.Id;
            UserProfile.Email = user.UserName;
            UserProfile.MealsCount = Meals.Count();

            if (Profile != null)
            {
                UserProfile.Profile = Profile;
            }

            UserProfiles.Add(UserProfile);
        }

        UserProfiles.Sort((x, y) => y.MealsCount.CompareTo(x.MealsCount));
    }
}
