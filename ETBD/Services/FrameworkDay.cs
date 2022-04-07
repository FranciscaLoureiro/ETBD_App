namespace ETBD.Services;

public class FrameworkDay
{
    public DateTime Date { get; set; }
    public List<Action> ActionsTaken { get; set; }
    public List<Action> ActionsNotTaken { get; set; }
    public List<Food> Foods { get; set; }
    public bool Success { get; set; }

    public FrameworkDay()
    {
    }
}
