namespace ETBD.Data.Entities;

public class ActionFood
{
    public int Id { get; set; }

    public Action Action { get; set; }
    public int ActionId { get; set; }

    public Food Food { get; set; }
    public int FoodId { get; set; }
}
