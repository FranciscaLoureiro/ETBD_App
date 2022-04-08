namespace ETBD.Services;

public class FastingDay
{
    public DateTime Date { get; set; }
    public List<Meal> Meals { get; set; }
    public string? FastingPeriod { get; set; }

    public FastingDay()
    {
    }
}
