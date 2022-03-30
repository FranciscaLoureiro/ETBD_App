namespace ETBD.Data.Entities;

public class Meal
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public IdentityUser User { get; set; }
    public string UserId { get; set; }
}
