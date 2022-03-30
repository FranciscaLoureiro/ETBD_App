namespace ETBD.Data.Entities;

public class Profile
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public int Age { get; set; }

    public decimal Weight { get; set; }

    public decimal Height { get; set; }

    public decimal BMI { get; set; }

    public IdentityUser User { get; set; }
    public string UserId { get; set; }
}
