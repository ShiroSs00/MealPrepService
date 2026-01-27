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
        public DbSet<Meal> Meals { get; set; }
        
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

            // THÊM PHẦN NÀY: Decimal precision cho Meal nutrition
            modelBuilder.Entity<Meal>()
                .Property(m => m.CaloriesPerServing)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Meal>()
                .Property(m => m.ProteinG)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Meal>()
                .Property(m => m.CarbsG)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Meal>()
                .Property(m => m.FatG)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Meal>()
                .Property(m => m.FiberG)
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

            // THÊM PHẦN NÀY: Indexes cho Meal
            modelBuilder.Entity<Meal>()
                .HasIndex(m => m.Category);

            modelBuilder.Entity<Meal>()
                .HasIndex(m => m.Cuisine);

            modelBuilder.Entity<Meal>()
                .HasIndex(m => m.IsActive);

            // Indexes đã có
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            modelBuilder.Entity<NutritionProfile>()
                .HasIndex(np => new { np.UserId, np.IsActive });

            modelBuilder.Entity<MealReview>()
                .HasIndex(mr => new { mr.OrderId, mr.DayNumber, mr.MealType });

            // THÊM PHẦN NÀY: SEED DATA cho Meals
            SeedMealData(modelBuilder);
        }

        private void SeedMealData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().HasData(
                // ========== BREAKFAST ==========
                new Meal
                {
                    Id = 1,
                    Name = "Phở Bò Truyền Thống",
                    Description = "Phở bò với nước dùng thơm ngon, bánh phở tươi và thịt bò tái",
                    Category = "Breakfast",
                    Cuisine = "Vietnamese",
                    CaloriesPerServing = 450,
                    ProteinG = 25,
                    CarbsG = 55,
                    FatG = 12,
                    FiberG = 3,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 45,
                    Budget = "Medium",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Bánh phở, thịt bò, hành tây, gừng, quế, hồi, muối, nước mắm",
                    Instructions = "Ninh xương bò 3-4h, thái thịt bò mỏng, trụng bánh phở, múc nước dùng và cho thịt tái lên trên",
                    ImagePath = "/images/pho-bo.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "traditional,vietnamese,high-protein"
                },

                new Meal
                {
                    Id = 2,
                    Name = "Bánh Mì Thịt Nướng",
                    Description = "Bánh mì giòn với thịt nướng thơm, rau sống và nước sốt đặc biệt",
                    Category = "Breakfast",
                    Cuisine = "Vietnamese",
                    CaloriesPerServing = 380,
                    ProteinG = 18,
                    CarbsG = 45,
                    FatG = 15,
                    FiberG = 4,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 20,
                    Budget = "Low",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = false,
                    IsDairyFree = true,
                    ContainsAllergens = "Gluten",
                    Ingredients = "Bánh mì, thịt heo nướng, dưa chuột, rau thơm, tương ớt",
                    Instructions = "Nướng thịt, cắt bánh mì, kẹp thịt và rau vào bánh mì",
                    ImagePath = "/images/banh-mi.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "quick,portable,vietnamese"
                },

                new Meal
                {
                    Id = 3,
                    Name = "Oatmeal với Berry",
                    Description = "Yến mạch bổ dưỡng với các loại berry tươi và hạt",
                    Category = "Breakfast",
                    Cuisine = "Western",
                    CaloriesPerServing = 320,
                    ProteinG = 12,
                    CarbsG = 58,
                    FatG = 8,
                    FiberG = 10,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 10,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Yến mạch, sữa hạnh nhân, blueberry, strawberry, hạt chia, mật ong",
                    Instructions = "Nấu yến mạch với sữa hạnh nhân, cho berry và hạt chia lên trên",
                    ImagePath = "/images/oatmeal-berry.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "healthy,high-fiber,vegetarian"
                },

                new Meal
                {
                    Id = 4,
                    Name = "Trứng Chiên Rau Củ",
                    Description = "Trứng chiên với nhiều loại rau củ bổ dưỡng",
                    Category = "Breakfast",
                    Cuisine = "Asian",
                    CaloriesPerServing = 280,
                    ProteinG = 20,
                    CarbsG = 15,
                    FatG = 18,
                    FiberG = 5,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 15,
                    Budget = "Low",
                    IsVegetarian = true,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Trứng gà, cà rốt, đậu cove, hành tây, tỏi, dầu ăn",
                    Instructions = "Xào rau củ, đánh trứng và chiên cùng rau",
                    ImagePath = "/images/egg-veggie.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,vegetarian,low-carb"
                },

                new Meal
                {
                    Id = 5,
                    Name = "Smoothie Xanh Detox",
                    Description = "Sinh tố xanh giàu vitamin và chất xơ",
                    Category = "Breakfast",
                    Cuisine = "Western",
                    CaloriesPerServing = 180,
                    ProteinG = 8,
                    CarbsG = 35,
                    FatG = 4,
                    FiberG = 8,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 5,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Rau bina, chuối, táo xanh, gừng, chanh, nước dừa",
                    Instructions = "Cho tất cả vào máy xay, xay nhuyễn và thưởng thức",
                    ImagePath = "/images/green-smoothie.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "detox,low-calorie,vegan"
                },

                // ========== LUNCH ==========
                new Meal
                {
                    Id = 6,
                    Name = "Cơm Gà Nướng Lemongrass",
                    Description = "Cơm trắng với gà nướng sả thơm lừng",
                    Category = "Lunch",
                    Cuisine = "Vietnamese",
                    CaloriesPerServing = 520,
                    ProteinG = 35,
                    CarbsG = 65,
                    FatG = 12,
                    FiberG = 3,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 40,
                    Budget = "Medium",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Thịt gà, sả, tỏi, gia vị, cơm trắng, rau sống",
                    Instructions = "Ướp gà với sả và gia vị, nướng chín, ăn kèm cơm và rau",
                    ImagePath = "/images/com-ga-nuong.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,vietnamese,balanced"
                },

                new Meal
                {
                    Id = 7,
                    Name = "Salad Gà Quinoa",
                    Description = "Salad gà nướng với quinoa và rau xanh",
                    Category = "Lunch",
                    Cuisine = "Western",
                    CaloriesPerServing = 420,
                    ProteinG = 30,
                    CarbsG = 35,
                    FatG = 18,
                    FiberG = 8,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 25,
                    Budget = "High",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Thịt gà, quinoa, rau xà lách, cà chua, dưa chuột, dressing olive oil",
                    Instructions = "Nấu quinoa, nướng gà, trộn salad với dressing",
                    ImagePath = "/images/chicken-quinoa-salad.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "superfood,high-protein,low-calorie"
                },

                new Meal
                {
                    Id = 8,
                    Name = "Pad Thai Chay",
                    Description = "Pad Thai chay với đậu hũ và rau củ",
                    Category = "Lunch",
                    Cuisine = "Asian",
                    CaloriesPerServing = 380,
                    ProteinG = 15,
                    CarbsG = 55,
                    FatG = 12,
                    FiberG = 6,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 30,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Bánh phở khô, đậu hũ, giá đỗ, cà rốt, tương ớt chay",
                    Instructions = "Xào đậu hũ, trụng bánh phở, xào chung với rau và gia vị",
                    ImagePath = "/images/pad-thai-chay.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "vegetarian,asian,balanced"
                },

                // ========== DINNER ==========
                new Meal
                {
                    Id = 9,
                    Name = "Cá Hồi Nướng Teriyaki",
                    Description = "Cá hồi nướng với sốt teriyaki và rau củ",
                    Category = "Dinner",
                    Cuisine = "Asian",
                    CaloriesPerServing = 480,
                    ProteinG = 40,
                    CarbsG = 25,
                    FatG = 25,
                    FiberG = 4,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 35,
                    Budget = "High",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = false,
                    IsDairyFree = true,
                    ContainsAllergens = "Soy,Gluten",
                    Ingredients = "Cá hồi, sốt teriyaki, súp lơ xanh, cà rốt, gạo lứt",
                    Instructions = "Ướp cá với sốt teriyaki, nướng cá, hấp rau củ",
                    ImagePath = "/images/salmon-teriyaki.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "omega-3,high-protein,premium"
                },

                new Meal
                {
                    Id = 10,
                    Name = "Thịt Bò Xào Lúc Lắc",
                    Description = "Thịt bò thăn xào với khoai tây và rau củ",
                    Category = "Dinner",
                    Cuisine = "Vietnamese",
                    CaloriesPerServing = 550,
                    ProteinG = 38,
                    CarbsG = 35,
                    FatG = 28,
                    FiberG = 5,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 30,
                    Budget = "High",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Thịt bò thăn, khoai tây, hành tây, tiêu đen, tỏi",
                    Instructions = "Thái thịt bò hạt lựu, ướp gia vị, xào nhanh tay",
                    ImagePath = "/images/bo-luc-lac.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,vietnamese,premium"
                },

                // ========== SNACK ==========
                new Meal
                {
                    Id = 11,
                    Name = "Greek Yogurt Hạt Điều",
                    Description = "Sữa chua Hy Lạp với hạt điều và mật ong",
                    Category = "Snack",
                    Cuisine = "Western",
                    CaloriesPerServing = 220,
                    ProteinG = 15,
                    CarbsG = 18,
                    FatG = 12,
                    FiberG = 3,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 5,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = false,
                    ContainsAllergens = "Dairy,Nuts",
                    Ingredients = "Greek yogurt, hạt điều, mật ong, quế bột",
                    Instructions = "Trộn tất cả các nguyên liệu với nhau",
                    ImagePath = "/images/greek-yogurt.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,quick,healthy"
                },

                new Meal
                {
                    Id = 12,
                    Name = "Trail Mix Energy",
                    Description = "Hỗn hợp hạt và trái cây khô bổ dưỡng",
                    Category = "Snack",
                    Cuisine = "Western",
                    CaloriesPerServing = 180,
                    ProteinG = 6,
                    CarbsG = 20,
                    FatG = 10,
                    FiberG = 4,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 2,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "Nuts",
                    Ingredients = "Hạnh nhân, nho khô, hạt điều, hạt bí ngô",
                    Instructions = "Trộn tất cả các loại hạt và trái cây khô",
                    ImagePath = "/images/trail-mix.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "portable,energy,nuts"
                },

                // ========== THÊM MỘT SỐ MÓN NỮA ==========
                new Meal
                {
                    Id = 13,
                    Name = "Bún Bò Huế",
                    Description = "Bún bò Huế truyền thống với nước dùng đậm đà",
                    Category = "Lunch",
                    Cuisine = "Vietnamese",
                    CaloriesPerServing = 480,
                    ProteinG = 28,
                    CarbsG = 60,
                    FatG = 15,
                    FiberG = 4,
                    DifficultyLevel = "Hard",
                    PrepTimeMinutes = 120,
                    Budget = "Medium",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Bún tươi, thịt bò, chả cá, tôm khô, sả, tỏi, ớt",
                    Instructions = "Ninh xương bò, nấu nước dùng, trụng bún và cho topping",
                    ImagePath = "/images/bun-bo-hue.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "traditional,vietnamese,spicy"
                },

                new Meal
                {
                    Id = 14,
                    Name = "Buddha Bowl",
                    Description = "Tô thức ăn chay đầy màu sắc và dinh dưỡng",
                    Category = "Lunch",
                    Cuisine = "Western",
                    CaloriesPerServing = 380,
                    ProteinG = 18,
                    CarbsG = 45,
                    FatG = 16,
                    FiberG = 12,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 20,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Quinoa, cải kale, cà chua, bơ, đậu đen, hạt bí ngô",
                    Instructions = "Nấu quinoa, chuẩn bị các loại rau, xếp vào tô và thưởng thức",
                    ImagePath = "/images/buddha-bowl.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "superfood,colorful,vegan"
                },

                new Meal
                {
                    Id = 15,
                    Name = "Protein Pancakes",
                    Description = "Bánh pancake protein cho bữa sáng bổ dưỡng",
                    Category = "Breakfast",
                    Cuisine = "Western",
                    CaloriesPerServing = 350,
                    ProteinG = 25,
                    CarbsG = 40,
                    FatG = 10,
                    FiberG = 6,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 15,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = false,
                    ContainsAllergens = "Dairy",
                    Ingredients = "Protein powder, yến mạch, trứng, chuối, sữa tươi",
                    Instructions = "Xay nhuyễn tất cả, đổ vào chảo và chiên như pancake thông thường",
                    ImagePath = "/images/protein-pancakes.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,fitness,breakfast"
                },

                new Meal
                {
                    Id = 16,
                    Name = "Tom Yum Goong",
                    Description = "Canh chua cay Thái Lan với tôm tươi",
                    Category = "Dinner",
                    Cuisine = "Asian",
                    CaloriesPerServing = 200,
                    ProteinG = 20,
                    CarbsG = 12,
                    FatG = 8,
                    FiberG = 3,
                    DifficultyLevel = "Medium",
                    PrepTimeMinutes = 25,
                    Budget = "Medium",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "Shellfish",
                    Ingredients = "Tôm tươi, sả, lá chanh, ớt, nấm, cà chua",
                    Instructions = "Nấu nước dùng với sả và lá chanh, cho tôm và rau vào nấu",
                    ImagePath = "/images/tom-yum.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "low-calorie,thai,spicy"
                },

                new Meal
                {
                    Id = 17,
                    Name = "Energy Balls",
                    Description = "Viên năng lượng từ hạt và trái cây khô",
                    Category = "Snack",
                    Cuisine = "Western",
                    CaloriesPerServing = 150,
                    ProteinG = 5,
                    CarbsG = 20,
                    FatG = 7,
                    FiberG = 4,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 10,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "Nuts",
                    Ingredients = "Chà là, hạnh nhân, hạt chia, bơ đậu phộng, bột ca cao",
                    Instructions = "Xay nhuyễn tất cả, vo thành từng viên nhỏ",
                    ImagePath = "/images/energy-balls.jpg",
                    GoodForWeightLoss = false,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "raw,energy,portable"
                },

                new Meal
                {
                    Id = 18,
                    Name = "Lẩu Thái Chay",
                    Description = "Lẩu Thái chay với đậu hũ và rau củ",
                    Category = "Dinner",
                    Cuisine = "Asian",
                    CaloriesPerServing = 280,
                    ProteinG = 15,
                    CarbsG = 25,
                    FatG = 12,
                    FiberG = 8,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 20,
                    Budget = "Low",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Đậu hũ, nấm, bắp cải, cà rốt, sả, ớt, nước dùng chay",
                    Instructions = "Nấu nước dùng chay, cho rau củ và đậu hũ vào nấu",
                    ImagePath = "/images/lau-thai-chay.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "vegetarian,thai,low-calorie"
                },

                new Meal
                {
                    Id = 19,
                    Name = "Chia Pudding",
                    Description = "Pudding hạt chia với sữa dừa và trái cây",
                    Category = "Breakfast",
                    Cuisine = "Western",
                    CaloriesPerServing = 250,
                    ProteinG = 8,
                    CarbsG = 25,
                    FatG = 15,
                    FiberG = 12,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 5,
                    Budget = "Medium",
                    IsVegetarian = true,
                    IsVegan = true,
                    IsGlutenFree = true,
                    IsDairyFree = true,
                    ContainsAllergens = "",
                    Ingredients = "Hạt chia, sữa dừa, mật ong, vanilla, mango",
                    Instructions = "Ngâm hạt chia qua đêm, cho topping trái cây",
                    ImagePath = "/images/chia-pudding.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = false,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "superfood,overnight,high-fiber"
                },

                new Meal
                {
                    Id = 20,
                    Name = "Grilled Chicken Wrap",
                    Description = "Bánh wrap gà nướng với rau xanh tươi mát",
                    Category = "Lunch",
                    Cuisine = "Western",
                    CaloriesPerServing = 400,
                    ProteinG = 32,
                    CarbsG = 35,
                    FatG = 16,
                    FiberG = 6,
                    DifficultyLevel = "Easy",
                    PrepTimeMinutes = 20,
                    Budget = "Medium",
                    IsVegetarian = false,
                    IsVegan = false,
                    IsGlutenFree = false,
                    IsDairyFree = true,
                    ContainsAllergens = "Gluten",
                    Ingredients = "Tortilla wrap, gà nướng, xà lách, cà chua, dưa chuột, avocado",
                    Instructions = "Nướng gà, chuẩn bị rau, cuốn vào tortilla",
                    ImagePath = "/images/chicken-wrap.jpg",
                    GoodForWeightLoss = true,
                    GoodForMuscleGain = true,
                    GoodForMaintain = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    Tags = "high-protein,portable,balanced"
                }
            );
        }
    }
}
