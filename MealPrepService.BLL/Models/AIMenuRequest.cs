using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepService.BLL.Models
{
    public class AIMenuRequest
    {
        // User Profile Info
        public int UserId { get; set; }
        public int NutritionProfileId { get; set; }

        // Nutrition Goals
        public string Goal { get; set; } = ""; // WeightLoss, MuscleGain, Maintain
        public double TargetCalories { get; set; }
        public double TargetProteinG { get; set; }
        public double TargetCarbsG { get; set; }
        public double TargetFatG { get; set; }
        public int MealsPerDay { get; set; } = 3;

        // Preferences
        public string Budget { get; set; } = "Medium"; // Low, Medium, High
        public string CuisinePreferences { get; set; } = ""; // "Vietnamese,Asian,Western"
        public string Allergies { get; set; } = ""; // "Dairy,Nuts,Shellfish"
        public bool IsVegetarian { get; set; } = false;
        public bool IsVegan { get; set; } = false;

        // Week Planning
        public DateTime WeekStartDate { get; set; }
        public int WeekNumber { get; set; }

        // Subscription Plan
        public string PlanType { get; set; } = "Basic"; // Basic, Premium
        public int TotalMealsPerWeek { get; set; } = 21; // 21 for Basic, 35 for Premium
    }
}
