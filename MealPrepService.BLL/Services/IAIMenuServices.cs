using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPrepService.BLL.Models;

namespace MealPrepService.BLL.Services
{
    public interface IAIMenuService
    {
        // Main AI Menu Generation
        Task<AIMenuResponse> GenerateWeeklyMenuAsync(AIMenuRequest request);

        // Helper Methods
        Task<List<MealDto>> SelectMealsForCategoryAsync(
            string category,
            string goal,
            string budget,
            string cuisinePreferences,
            string allergies,
            bool isVegetarian,
            bool isVegan,
            int mealsNeeded);

        Task<MealDto> GetRandomMealForCategoryAsync(
            string category,
            List<int> excludeMealIds = null);

        // Nutrition Validation
        bool ValidateNutritionGoals(WeeklyMenuDto menu, AIMenuRequest request);
        MenuNutritionSummary CalculateNutritionSummary(WeeklyMenuDto menu, AIMenuRequest request);

        // Menu Optimization
        Task<WeeklyMenuDto> OptimizeMenuForGoalsAsync(WeeklyMenuDto menu, AIMenuRequest request);
    }
}
