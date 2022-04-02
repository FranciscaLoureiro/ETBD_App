namespace ETBD.Data.Entities;

public class FavouriteList
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public Food Food { get; set; }
    [DisplayName("Food Name")]
    public int FoodId { get; set; }

    public IdentityUser User { get; set; }
    public string UserId { get; set; }

    public FavouriteList()
    {
        Date = DateTime.Now;    
    }
}
