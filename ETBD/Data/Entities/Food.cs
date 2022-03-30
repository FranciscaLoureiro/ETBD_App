namespace ETBD.Data.Entities;

public class Food
{
    public int Id { get; set; }

    [DisplayName("Food Name")]
    [Required]
    public string Name { get; set; }

    public Category Category { get; set; }
    public int CategoryId { get; set; }
}
