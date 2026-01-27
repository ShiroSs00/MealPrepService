using System.ComponentModel.DataAnnotations;

namespace MealPrepService.Web.Models
{
    public class PlanSelectionViewModel
    {
        public int UserId { get; set; }
        public int NutritionProfileId { get; set; }

        [Required]
        [Display(Name = "Goal")]
        public string Goal { get; set; } = "";

        [Required]
        [Display(Name = "Plan Type")]
        public string PlanType { get; set; } = "Basic"; // Basic, Premium

        [Required]
        [Display(Name = "Budget")]
        public string Budget { get; set; } = "Medium";

        [Display(Name = "Week Start Date")]
        [DataType(DataType.Date)]
        public DateTime WeekStartDate { get; set; } = DateTime.Today.AddDays(1);

        [Display(Name = "Cuisine Preferences")]
        public string CuisinePreferences { get; set; } = "";

        [Display(Name = "Allergies")]
        public string Allergies { get; set; } = "";

        [Display(Name = "Vegetarian")]
        public bool IsVegetarian { get; set; }

        [Display(Name = "Vegan")]
        public bool IsVegan { get; set; }

        [Range(3, 5)]
        [Display(Name = "Meals Per Day")]
        public int MealsPerDay { get; set; } = 3;

        // For display purposes
        public int TotalMealsPerWeek => PlanType == "Premium" ? 35 : 21;
        public string WeekDateRange => $"{WeekStartDate:MMM dd} - {WeekStartDate.AddDays(6):MMM dd, yyyy}";
    }
}

