using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPrepService.DAL.Entities;

namespace MealPrepService.DAL.Repositories
{
    public interface IMealRepository
    {
        // Basic CRUD Operations
        Task<IEnumerable<Meal>> GetAllMealsAsync();
        Task<Meal?> GetMealByIdAsync(int id);
        Task<Meal> AddMealAsync(Meal meal);
        Task<Meal> UpdateMealAsync(Meal meal);
        Task<bool> DeleteMealAsync(int id);
        Task<bool> MealExistsAsync(int id);

        // AI Menu Selection Methods
        Task<IEnumerable<Meal>> GetMealsByCategoryAsync(string category);
        Task<IEnumerable<Meal>> GetMealsByCuisineAsync(string cuisine);
        Task<IEnumerable<Meal>> GetMealsByGoalAsync(string goal);
        Task<IEnumerable<Meal>> GetMealsByBudgetAsync(string budget);

        // Advanced Filtering for AI
        Task<IEnumerable<Meal>> GetMealsForAISelectionAsync(
            string category,
            string goal,
            string budget,
            string cuisine = "",
            string allergies = "",
            bool isVegetarian = false,
            bool isVegan = false);

        // Random Selection for AI
        Task<IEnumerable<Meal>> GetRandomMealsAsync(int count, string category = "");

        // Nutrition-based queries
        Task<IEnumerable<Meal>> GetMealsByCalorieRangeAsync(double minCalories, double maxCalories);
        Task<IEnumerable<Meal>> GetHighProteinMealsAsync(double minProtein);
    }
}
