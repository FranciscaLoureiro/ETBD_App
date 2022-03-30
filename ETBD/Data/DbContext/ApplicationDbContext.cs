namespace ETBDApp.Data
{
    
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<Food> Foods { get; set; }  
        public DbSet<ActionFood> ActionFoods { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<FoodMeal> FoodMeals { get; set; }
        public DbSet<BlackList> BlackLists { get; set;  }
        public DbSet<FavouriteList> FavouriteLists { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

  
}
