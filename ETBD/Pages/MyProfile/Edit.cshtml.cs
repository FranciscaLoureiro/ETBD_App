namespace ETBD.Pages.MyProfile;

[BindProperties]
public class EditModel : PageModel
{
    public Profile MyProfile { get; set; }
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public string UserId { get; set; }

    public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public IActionResult OnGet(int? id)
    {
        MyProfile = _context.Profiles.FirstOrDefault(m => m.Id == id);

        if (MyProfile == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        decimal weight = Convert.ToDecimal(MyProfile.Weight);
        decimal height = Convert.ToDecimal(MyProfile.Height);
        DateTime birthDate = Convert.ToDateTime(MyProfile.BirthDate);

        UserId = _userManager.GetUserId(User);
        var ibm = Math.Round(weight / (height * height), 2);
        var age = Math.Round((DateTime.Now - birthDate).TotalDays / 365);

        MyProfile.UserId = UserId;
        MyProfile.Age = Convert.ToInt32(age);
        MyProfile.BMI = Convert.ToDecimal(ibm);

        _context.Attach(MyProfile).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        return RedirectToPage("Index");

    }
}
