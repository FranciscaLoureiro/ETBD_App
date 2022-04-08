namespace ETBD.Services;

public class UserProfile
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public Profile? Profile { get; set; }
    public int MealsCount { get; set; }

    public UserProfile()
    {
    }
}
