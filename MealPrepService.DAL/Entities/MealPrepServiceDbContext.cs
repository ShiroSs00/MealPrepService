using Microsoft.EntityFrameworkCore;

namespace MealPrepService.DAL.Entities
{
    public class MealPrepDbContext : DbContext
    {
        public MealPrepDbContext(DbContextOptions<MealPrepDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<NutritionProfile> NutritionProfiles { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MealReview> MealReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision configuration
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2); // Max 18 digits, 2 decimal places

            modelBuilder.Entity<SubscriptionPlan>()
                .Property(sp => sp.PricePerWeek)
                .HasPrecision(18, 2);

            // User - NutritionProfile: One-to-Many
            modelBuilder.Entity<User>()
                .HasMany(u => u.NutritionProfiles)
                .WithOne(np => np.User)
                .HasForeignKey(np => np.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Order: One-to-Many
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // NutritionProfile - Order: One-to-Many
            modelBuilder.Entity<NutritionProfile>()
                .HasMany(np => np.Orders)
                .WithOne(o => o.NutritionProfile)
                .HasForeignKey(o => o.NutritionProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // SubscriptionPlan - Order: One-to-Many
            modelBuilder.Entity<SubscriptionPlan>()
                .HasMany(sp => sp.Orders)
                .WithOne(o => o.SubscriptionPlan)
                .HasForeignKey(o => o.SubscriptionPlanId)
                .OnDelete(DeleteBehavior.SetNull);

            // Order - MealReview: One-to-Many
            modelBuilder.Entity<Order>()
                .HasMany(o => o.MealReviews)
                .WithOne(mr => mr.Order)
                .HasForeignKey(mr => mr.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - MealReview: One-to-Many
            modelBuilder.Entity<User>()
                .HasMany<MealReview>()
                .WithOne(mr => mr.User)
                .HasForeignKey(mr => mr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            modelBuilder.Entity<NutritionProfile>()
                .HasIndex(np => new { np.UserId, np.IsActive });

            modelBuilder.Entity<MealReview>()
                .HasIndex(mr => new { mr.OrderId, mr.DayNumber, mr.MealType });
        }
    }
}
