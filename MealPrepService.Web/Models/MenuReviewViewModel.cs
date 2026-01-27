using MealPrepService.BLL.Models;

namespace MealPrepService.Web.Models
{
    public class MenuReviewViewModel
    {
        public int UserId { get; set; }
        public string PlanType { get; set; } = "";
        public DateTime WeekStartDate { get; set; }
        public WeeklyMenuDto WeeklyMenu { get; set; } = new();
        public MenuNutritionSummary NutritionSummary { get; set; } = new();
        public DateTime GeneratedAt { get; set; }

        // For display
        public string WeekDateRange => $"{WeekStartDate:MMM dd} - {WeekStartDate.AddDays(6):MMM dd, yyyy}";
        public bool IsPremiumPlan => PlanType == "Premium";
    }
}

