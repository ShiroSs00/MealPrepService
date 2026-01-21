using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPrepService.DAL.Entities
{
    [Table("NutritionProfiles")]
    public class NutritionProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        // Activity Level
        [Required, MaxLength(20)]
        public string ActivityLevel { get; set; } = ""; // Sedentary, Light, Moderate, Active, VeryActive

        // Goal
        [Required, MaxLength(20)]
        public string Goal { get; set; } = ""; // WeightLoss, Maintain, MuscleGain

        // Calculated Nutrition Values
        public double BMR { get; set; } // Basal Metabolic Rate
        public double TDEE { get; set; } // Total Daily Energy Expenditure
        public double TargetCalories { get; set; } // Adjusted based on goal

        // Macros (grams per day)
        public double TargetProteinG { get; set; }
        public double TargetCarbsG { get; set; }
        public double TargetFatG { get; set; }

        // Preferences
        [MaxLength(500)]
        public string Allergies { get; set; } = ""; // Comma-separated: "Dairy,Nuts,Shellfish"

        [MaxLength(500)]
        public string CuisinePreferences { get; set; } = ""; // Comma-separated: "Vietnamese,Asian,Western"

        [MaxLength(20)]
        public string Budget { get; set; } = ""; // Low, Medium, High

        public int MealsPerDay { get; set; } = 3; // Default 3 meals

        public bool IsActive { get; set; } = true; // Current active profile
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = [];
    }
}