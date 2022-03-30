namespace ETBD.Data.Entities;

public class Action
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
