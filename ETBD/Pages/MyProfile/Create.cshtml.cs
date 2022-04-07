namespace ETBD.Pages.MyProfile;

[BindProperties]
public class CreateModel : PageModel
{
    public Profile Profile { get; set; }
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public string UserId { get; set; }

    public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public void OnGet()
    {
        UserId = _userManager.GetUserId(User);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        decimal weight = Convert.ToDecimal(Profile.Weight);
        decimal height = Convert.ToDecimal(Profile.Height);
        DateTime birthDate = Convert.ToDateTime(Profile.BirthDate);

        UserId = _userManager.GetUserId(User);
        var ibm = Math.Round(weight / (height * height), 2);
        var age = Math.Round((DateTime.Now - birthDate).TotalDays / 365);

        Profile.UserId = UserId;
        Profile.Age = Convert.ToInt32(age);
        Profile.BMI = Convert.ToDecimal(ibm);

        _context.Profiles.Add(Profile);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
