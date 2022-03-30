namespace ETBD.Data.Entities;

public class Category
{
    public int Id { get; set; }

    [DisplayName("Category Name")]
    [Required]
    public string Name { get; set; }
}
