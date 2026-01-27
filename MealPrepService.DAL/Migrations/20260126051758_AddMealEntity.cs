using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealPrepService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMealEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cuisine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CaloriesPerServing = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    ProteinG = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    CarbsG = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    FatG = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    FiberG = table.Column<double>(type: "float(18)", precision: 18, scale: 2, nullable: false),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrepTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    IsDairyFree = table.Column<bool>(type: "bit", nullable: false),
                    ContainsAllergens = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GoodForWeightLoss = table.Column<bool>(type: "bit", nullable: false),
                    GoodForMuscleGain = table.Column<bool>(type: "bit", nullable: false),
                    GoodForMaintain = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Budget", "CaloriesPerServing", "CarbsG", "Category", "ContainsAllergens", "CreatedAt", "Cuisine", "Description", "DifficultyLevel", "FatG", "FiberG", "GoodForMaintain", "GoodForMuscleGain", "GoodForWeightLoss", "ImagePath", "Ingredients", "Instructions", "IsActive", "IsDairyFree", "IsGlutenFree", "IsVegan", "IsVegetarian", "Name", "PrepTimeMinutes", "ProteinG", "Tags", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Medium", 450.0, 55.0, "Breakfast", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1686), "Vietnamese", "Phở bò với nước dùng thơm ngon, bánh phở tươi và thịt bò tái", "Medium", 12.0, 3.0, true, true, false, "/images/pho-bo.jpg", "Bánh phở, thịt bò, hành tây, gừng, quế, hồi, muối, nước mắm", "Ninh xương bò 3-4h, thái thịt bò mỏng, trụng bánh phở, múc nước dùng và cho thịt tái lên trên", true, true, true, false, false, "Phở Bò Truyền Thống", 45, 25.0, "traditional,vietnamese,high-protein", null },
                    { 2, "Low", 380.0, 45.0, "Breakfast", "Gluten", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1691), "Vietnamese", "Bánh mì giòn với thịt nướng thơm, rau sống và nước sốt đặc biệt", "Easy", 15.0, 4.0, true, false, true, "/images/banh-mi.jpg", "Bánh mì, thịt heo nướng, dưa chuột, rau thơm, tương ớt", "Nướng thịt, cắt bánh mì, kẹp thịt và rau vào bánh mì", true, true, false, false, false, "Bánh Mì Thịt Nướng", 20, 18.0, "quick,portable,vietnamese", null },
                    { 3, "Medium", 320.0, 58.0, "Breakfast", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1695), "Western", "Yến mạch bổ dưỡng với các loại berry tươi và hạt", "Easy", 8.0, 10.0, true, false, true, "/images/oatmeal-berry.jpg", "Yến mạch, sữa hạnh nhân, blueberry, strawberry, hạt chia, mật ong", "Nấu yến mạch với sữa hạnh nhân, cho berry và hạt chia lên trên", true, true, true, true, true, "Oatmeal với Berry", 10, 12.0, "healthy,high-fiber,vegetarian", null },
                    { 4, "Low", 280.0, 15.0, "Breakfast", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1698), "Asian", "Trứng chiên với nhiều loại rau củ bổ dưỡng", "Easy", 18.0, 5.0, true, true, true, "/images/egg-veggie.jpg", "Trứng gà, cà rốt, đậu cove, hành tây, tỏi, dầu ăn", "Xào rau củ, đánh trứng và chiên cùng rau", true, true, true, false, true, "Trứng Chiên Rau Củ", 15, 20.0, "high-protein,vegetarian,low-carb", null },
                    { 5, "Medium", 180.0, 35.0, "Breakfast", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1702), "Western", "Sinh tố xanh giàu vitamin và chất xơ", "Easy", 4.0, 8.0, true, false, true, "/images/green-smoothie.jpg", "Rau bina, chuối, táo xanh, gừng, chanh, nước dừa", "Cho tất cả vào máy xay, xay nhuyễn và thưởng thức", true, true, true, true, true, "Smoothie Xanh Detox", 5, 8.0, "detox,low-calorie,vegan", null },
                    { 6, "Medium", 520.0, 65.0, "Lunch", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1705), "Vietnamese", "Cơm trắng với gà nướng sả thơm lừng", "Medium", 12.0, 3.0, true, true, false, "/images/com-ga-nuong.jpg", "Thịt gà, sả, tỏi, gia vị, cơm trắng, rau sống", "Ướp gà với sả và gia vị, nướng chín, ăn kèm cơm và rau", true, true, true, false, false, "Cơm Gà Nướng Lemongrass", 40, 35.0, "high-protein,vietnamese,balanced", null },
                    { 7, "High", 420.0, 35.0, "Lunch", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1709), "Western", "Salad gà nướng với quinoa và rau xanh", "Easy", 18.0, 8.0, true, true, true, "/images/chicken-quinoa-salad.jpg", "Thịt gà, quinoa, rau xà lách, cà chua, dưa chuột, dressing olive oil", "Nấu quinoa, nướng gà, trộn salad với dressing", true, true, true, false, false, "Salad Gà Quinoa", 25, 30.0, "superfood,high-protein,low-calorie", null },
                    { 8, "Medium", 380.0, 55.0, "Lunch", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1712), "Asian", "Pad Thai chay với đậu hũ và rau củ", "Medium", 12.0, 6.0, true, false, true, "/images/pad-thai-chay.jpg", "Bánh phở khô, đậu hũ, giá đỗ, cà rốt, tương ớt chay", "Xào đậu hũ, trụng bánh phở, xào chung với rau và gia vị", true, true, true, true, true, "Pad Thai Chay", 30, 15.0, "vegetarian,asian,balanced", null },
                    { 9, "High", 480.0, 25.0, "Dinner", "Soy,Gluten", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1716), "Asian", "Cá hồi nướng với sốt teriyaki và rau củ", "Medium", 25.0, 4.0, true, true, false, "/images/salmon-teriyaki.jpg", "Cá hồi, sốt teriyaki, súp lơ xanh, cà rốt, gạo lứt", "Ướp cá với sốt teriyaki, nướng cá, hấp rau củ", true, true, false, false, false, "Cá Hồi Nướng Teriyaki", 35, 40.0, "omega-3,high-protein,premium", null },
                    { 10, "High", 550.0, 35.0, "Dinner", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1719), "Vietnamese", "Thịt bò thăn xào với khoai tây và rau củ", "Medium", 28.0, 5.0, true, true, false, "/images/bo-luc-lac.jpg", "Thịt bò thăn, khoai tây, hành tây, tiêu đen, tỏi", "Thái thịt bò hạt lựu, ướp gia vị, xào nhanh tay", true, true, true, false, false, "Thịt Bò Xào Lúc Lắc", 30, 38.0, "high-protein,vietnamese,premium", null },
                    { 11, "Medium", 220.0, 18.0, "Snack", "Dairy,Nuts", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1723), "Western", "Sữa chua Hy Lạp với hạt điều và mật ong", "Easy", 12.0, 3.0, true, true, true, "/images/greek-yogurt.jpg", "Greek yogurt, hạt điều, mật ong, quế bột", "Trộn tất cả các nguyên liệu với nhau", true, false, true, false, true, "Greek Yogurt Hạt Điều", 5, 15.0, "high-protein,quick,healthy", null },
                    { 12, "Medium", 180.0, 20.0, "Snack", "Nuts", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1728), "Western", "Hỗn hợp hạt và trái cây khô bổ dưỡng", "Easy", 10.0, 4.0, true, true, false, "/images/trail-mix.jpg", "Hạnh nhân, nho khô, hạt điều, hạt bí ngô", "Trộn tất cả các loại hạt và trái cây khô", true, true, true, true, true, "Trail Mix Energy", 2, 6.0, "portable,energy,nuts", null },
                    { 13, "Medium", 480.0, 60.0, "Lunch", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1731), "Vietnamese", "Bún bò Huế truyền thống với nước dùng đậm đà", "Hard", 15.0, 4.0, true, true, false, "/images/bun-bo-hue.jpg", "Bún tươi, thịt bò, chả cá, tôm khô, sả, tỏi, ớt", "Ninh xương bò, nấu nước dùng, trụng bún và cho topping", true, true, true, false, false, "Bún Bò Huế", 120, 28.0, "traditional,vietnamese,spicy", null },
                    { 14, "Medium", 380.0, 45.0, "Lunch", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1735), "Western", "Tô thức ăn chay đầy màu sắc và dinh dưỡng", "Easy", 16.0, 12.0, true, false, true, "/images/buddha-bowl.jpg", "Quinoa, cải kale, cà chua, bơ, đậu đen, hạt bí ngô", "Nấu quinoa, chuẩn bị các loại rau, xếp vào tô và thưởng thức", true, true, true, true, true, "Buddha Bowl", 20, 18.0, "superfood,colorful,vegan", null },
                    { 15, "Medium", 350.0, 40.0, "Breakfast", "Dairy", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1738), "Western", "Bánh pancake protein cho bữa sáng bổ dưỡng", "Easy", 10.0, 6.0, true, true, true, "/images/protein-pancakes.jpg", "Protein powder, yến mạch, trứng, chuối, sữa tươi", "Xay nhuyễn tất cả, đổ vào chảo và chiên như pancake thông thường", true, false, true, false, true, "Protein Pancakes", 15, 25.0, "high-protein,fitness,breakfast", null },
                    { 16, "Medium", 200.0, 12.0, "Dinner", "Shellfish", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1742), "Asian", "Canh chua cay Thái Lan với tôm tươi", "Medium", 8.0, 3.0, true, false, true, "/images/tom-yum.jpg", "Tôm tươi, sả, lá chanh, ớt, nấm, cà chua", "Nấu nước dùng với sả và lá chanh, cho tôm và rau vào nấu", true, true, true, false, false, "Tom Yum Goong", 25, 20.0, "low-calorie,thai,spicy", null },
                    { 17, "Medium", 150.0, 20.0, "Snack", "Nuts", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1745), "Western", "Viên năng lượng từ hạt và trái cây khô", "Easy", 7.0, 4.0, true, true, false, "/images/energy-balls.jpg", "Chà là, hạnh nhân, hạt chia, bơ đậu phộng, bột ca cao", "Xay nhuyễn tất cả, vo thành từng viên nhỏ", true, true, true, true, true, "Energy Balls", 10, 5.0, "raw,energy,portable", null },
                    { 18, "Low", 280.0, 25.0, "Dinner", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1748), "Asian", "Lẩu Thái chay với đậu hũ và rau củ", "Easy", 12.0, 8.0, true, false, true, "/images/lau-thai-chay.jpg", "Đậu hũ, nấm, bắp cải, cà rốt, sả, ớt, nước dùng chay", "Nấu nước dùng chay, cho rau củ và đậu hũ vào nấu", true, true, true, true, true, "Lẩu Thái Chay", 20, 15.0, "vegetarian,thai,low-calorie", null },
                    { 19, "Medium", 250.0, 25.0, "Breakfast", "", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1751), "Western", "Pudding hạt chia với sữa dừa và trái cây", "Easy", 15.0, 12.0, true, false, true, "/images/chia-pudding.jpg", "Hạt chia, sữa dừa, mật ong, vanilla, mango", "Ngâm hạt chia qua đêm, cho topping trái cây", true, true, true, true, true, "Chia Pudding", 5, 8.0, "superfood,overnight,high-fiber", null },
                    { 20, "Medium", 400.0, 35.0, "Lunch", "Gluten", new DateTime(2026, 1, 26, 5, 17, 57, 105, DateTimeKind.Utc).AddTicks(1754), "Western", "Bánh wrap gà nướng với rau xanh tươi mát", "Easy", 16.0, 6.0, true, true, true, "/images/chicken-wrap.jpg", "Tortilla wrap, gà nướng, xà lách, cà chua, dưa chuột, avocado", "Nướng gà, chuẩn bị rau, cuốn vào tortilla", true, true, false, false, false, "Grilled Chicken Wrap", 20, 32.0, "high-protein,portable,balanced", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Category",
                table: "Meals",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_Cuisine",
                table: "Meals",
                column: "Cuisine");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_IsActive",
                table: "Meals",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
