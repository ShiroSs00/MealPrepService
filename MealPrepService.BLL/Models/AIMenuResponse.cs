using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepService.BLL.Models
{
    public class AIMenuResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public WeeklyMenuDto WeeklyMenu { get; set; } = new();
        public MenuNutritionSummary NutritionSummary { get; set; } = new();
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string MenuJsonData { get; set; } = ""; // For storing in Order.MenuJsonData
    }

    public class WeeklyMenuDto
    {
        public List<DailyMealDto> Days { get; set; } = new();
        public int TotalMeals { get; set; }
        public double TotalEstimatedCost { get; set; }
    }

    public class DailyMealDto
    {
        public int DayNumber { get; set; } // 1-7 (Monday to Sunday)
        public string DayName { get; set; } = ""; // "Monday", "Tuesday", etc.
        public DateTime Date { get; set; }
        public List<MealDto> Meals { get; set; } = new();
        public double DailyCalories { get; set; }
        public double DailyProtein { get; set; }
        public double DailyCarbs { get; set; }
        public double DailyFat { get; set; }
    }

    public class MealDto
    {
        public int MealId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = ""; // Breakfast, Lunch, Dinner, Snack
        public string Cuisine { get; set; } = "";

        // Nutrition
        public double CaloriesPerServing { get; set; }
        public double ProteinG { get; set; }
        public double CarbsG { get; set; }
        public double FatG { get; set; }
        public double FiberG { get; set; }

        // Additional Info
        public int PrepTimeMinutes { get; set; }
        public string DifficultyLevel { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public string Tags { get; set; } = "";

        // AI Selection Reason
        public string SelectionReason { get; set; } = ""; // "High protein for muscle gain", "Low calorie for weight loss"
    }

    public class MenuNutritionSummary
    {
        public double AverageCaloriesPerDay { get; set; }
        public double AverageProteinPerDay { get; set; }
        public double AverageCarbsPerDay { get; set; }
        public double AverageFatPerDay { get; set; }
        public double CalorieVariance { get; set; } // % difference from target
        public bool MeetsNutritionGoals { get; set; }
        public List<string> NutritionNotes { get; set; } = new();
    }
}
