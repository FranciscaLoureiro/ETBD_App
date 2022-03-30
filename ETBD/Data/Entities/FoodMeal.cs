namespace ETBD.Data.Entities;

public class FoodMeal
{
    public int Id { get; set; }

    public Food Food { get; set; }
    public int FoodId { get; set; }

    public Meal Meal { get; set; }
    public int MealId { get; set; }

    [Required]
    public string PortionUnit { get; set; }

    [Required]
    public decimal PortionQuantity { get; set; }
}
