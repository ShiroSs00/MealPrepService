using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPrepService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealPrepService.DAL.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly MealPrepDbContext _context;

        public MealRepository(MealPrepDbContext context)
        {
            _context = context;
        }

        // ==================== BASIC CRUD ====================

        public async Task<IEnumerable<Meal>> GetAllMealsAsync()
        {
            return await _context.Meals
                .Where(m => m.IsActive)
                .OrderBy(m => m.Category)
                .ThenBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await _context.Meals
                .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);
        }

        public async Task<Meal> AddMealAsync(Meal meal)
        {
            meal.CreatedAt = DateTime.UtcNow;
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return meal;
        }

        public async Task<Meal> UpdateMealAsync(Meal meal)
        {
            meal.UpdatedAt = DateTime.UtcNow;
            _context.Entry(meal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return meal;
        }

        public async Task<bool> DeleteMealAsync(int id)
        {
            var meal = await GetMealByIdAsync(id);
            if (meal == null) return false;

            // Soft delete
            meal.IsActive = false;
            meal.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MealExistsAsync(int id)
        {
            return await _context.Meals
                .AnyAsync(m => m.Id == id && m.IsActive);
        }

        // ==================== AI MENU SELECTION ====================

        public async Task<IEnumerable<Meal>> GetMealsByCategoryAsync(string category)
        {
            return await _context.Meals
                .Where(m => m.IsActive && m.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetMealsByCuisineAsync(string cuisine)
        {
            return await _context.Meals
                .Where(m => m.IsActive && m.Cuisine == cuisine)
                .ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetMealsByGoalAsync(string goal)
        {
            return await _context.Meals
                .Where(m => m.IsActive &&
                    (goal == "WeightLoss" && m.GoodForWeightLoss) ||
                    (goal == "MuscleGain" && m.GoodForMuscleGain) ||
                    (goal == "Maintain" && m.GoodForMaintain))
                .ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetMealsByBudgetAsync(string budget)
        {
            return await _context.Meals
                .Where(m => m.IsActive && m.Budget == budget)
                .ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetMealsForAISelectionAsync(
            string category,
            string goal,
            string budget,
            string cuisine = "",
            string allergies = "",
            bool isVegetarian = false,
            bool isVegan = false)
        {
            var query = _context.Meals
                .Where(m => m.IsActive && m.Category == category && m.Budget == budget);

            // Filter by goal
            if (goal == "WeightLoss")
                query = query.Where(m => m.GoodForWeightLoss);
            else if (goal == "MuscleGain")
                query = query.Where(m => m.GoodForMuscleGain);
            else if (goal == "Maintain")
                query = query.Where(m => m.GoodForMaintain);

            // Filter by cuisine preference
            if (!string.IsNullOrEmpty(cuisine))
            {
                var cuisines = cuisine.Split(',', StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(m => cuisines.Contains(m.Cuisine));
            }

            // Filter by dietary restrictions
            if (isVegan)
                query = query.Where(m => m.IsVegan);
            else if (isVegetarian)
                query = query.Where(m => m.IsVegetarian);

            // Filter by allergies
            if (!string.IsNullOrEmpty(allergies))
            {
                var allergenList = allergies.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim()).ToList();

                foreach (var allergen in allergenList)
                {
                    query = query.Where(m => !m.ContainsAllergens.Contains(allergen));
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetRandomMealsAsync(int count, string category = "")
        {
            var query = _context.Meals.Where(m => m.IsActive);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(m => m.Category == category);

            // Simple random selection (for production, use better randomization)
            return await query
                .OrderBy(m => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }

        // ==================== NUTRITION-BASED QUERIES ====================

        public async Task<IEnumerable<Meal>> GetMealsByCalorieRangeAsync(double minCalories, double maxCalories)
        {
            return await _context.Meals
                .Where(m => m.IsActive &&
                    m.CaloriesPerServing >= minCalories &&
                    m.CaloriesPerServing <= maxCalories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Meal>> GetHighProteinMealsAsync(double minProtein)
        {
            return await _context.Meals
                .Where(m => m.IsActive && m.ProteinG >= minProtein)
                .OrderByDescending(m => m.ProteinG)
                .ToListAsync();
        }
    }
}
