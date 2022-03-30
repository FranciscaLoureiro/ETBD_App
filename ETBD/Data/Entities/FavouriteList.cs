namespace ETBD.Data.Entities;

public class FavouriteList
{
    public int Id { get; set; }

    [DisplayName("Creation Date")]
    [Required]
    public DateTime Date { get; set; }

    public Food Food { get; set; }
    public int FoodId { get; set; }

    public IdentityUser User { get; set; }
    public string UserId { get; set; }
}
