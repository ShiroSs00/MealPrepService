using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MealPrepService.DAL.Entities
{
    [Table("Meals")]
    public class Meal
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = "";

        [MaxLength(1000)]
        public string Description { get; set; } = "";

        [Required, MaxLength(50)]
        public string Category { get; set; } = ""; // Breakfast, Lunch, Dinner, Snack

        [Required, MaxLength(100)]
        public string Cuisine { get; set; } = ""; // Vietnamese, Asian, Western, etc.

        // Nutrition per serving
        public double CaloriesPerServing { get; set; }
        public double ProteinG { get; set; }
        public double CarbsG { get; set; }
        public double FatG { get; set; }
        public double FiberG { get; set; }

        // Meal properties
        [Required, MaxLength(20)]
        public string DifficultyLevel { get; set; } = ""; // Easy, Medium, Hard

        public int PrepTimeMinutes { get; set; }

        [Required, MaxLength(20)]
        public string Budget { get; set; } = ""; // Low, Medium, High

        // Dietary restrictions
        public bool IsVegetarian { get; set; } = false;
        public bool IsVegan { get; set; } = false;
        public bool IsGlutenFree { get; set; } = false;
        public bool IsDairyFree { get; set; } = false;

        // Common allergens (comma-separated)
        [MaxLength(500)]
        public string ContainsAllergens { get; set; } = ""; // "Dairy,Nuts,Shellfish"

        // Ingredients and instructions
        [MaxLength(2000)]
        public string Ingredients { get; set; } = "";

        [MaxLength(3000)]
        public string Instructions { get; set; } = "";

        [MaxLength(500)]
        public string ImagePath { get; set; } = "";

        // Suitability for goals
        public bool GoodForWeightLoss { get; set; } = false;
        public bool GoodForMuscleGain { get; set; } = false;
        public bool GoodForMaintain { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // For seeding/demo purposes
        [MaxLength(100)]
        public string Tags { get; set; } = ""; // "high-protein,low-carb,quick"
    }
}
