using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MealPrepService.BLL.Models;
using MealPrepService.DAL.Repositories;
using MealPrepService.DAL.Entities;
using System.Text.Json;

namespace MealPrepService.BLL.Services
{
    public class AIMenuService : IAIMenuService
    {
        private readonly IMealRepository _mealRepository;

        public AIMenuService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<AIMenuResponse> GenerateWeeklyMenuAsync(AIMenuRequest request)
        {
            try
            {
                // ========== STEP 1: GENERATE WEEKLY MENU ==========
                var weeklyMenu = new WeeklyMenuDto();

                // Calculate meals per day by category
                var (breakfastPerDay, lunchPerDay, dinnerPerDay, snackPerDay) =
                    CalculateMealsPerDay(request.MealsPerDay, request.PlanType);

                // Generate 7 days of meals
                for (int day = 1; day <= 7; day++)
                {
                    var dailyMeal = await GenerateDailyMenuAsync(
                        day,
                        request.WeekStartDate.AddDays(day - 1),
                        breakfastPerDay, lunchPerDay, dinnerPerDay, snackPerDay,
                        request);

                    weeklyMenu.Days.Add(dailyMeal);
                }

                // ========== STEP 2: CALCULATE NUTRITION SUMMARY ==========
                var nutritionSummary = CalculateNutritionSummary(weeklyMenu, request);

                // ========== STEP 3: OPTIMIZE IF NEEDED ==========
                if (!nutritionSummary.MeetsNutritionGoals)
                {
                    weeklyMenu = await OptimizeMenuForGoalsAsync(weeklyMenu, request);
                    nutritionSummary = CalculateNutritionSummary(weeklyMenu, request);
                }

                // ========== STEP 4: PREPARE RESPONSE ==========
                weeklyMenu.TotalMeals = weeklyMenu.Days.Sum(d => d.Meals.Count);
                weeklyMenu.TotalEstimatedCost = CalculateEstimatedCost(weeklyMenu, request.Budget);

                var response = new AIMenuResponse
                {
                    Success = true,
                    Message = "Weekly menu generated successfully",
                    WeeklyMenu = weeklyMenu,
                    NutritionSummary = nutritionSummary,
                    MenuJsonData = JsonSerializer.Serialize(weeklyMenu),
                    GeneratedAt = DateTime.UtcNow
                };

                return response;
            }
            catch (Exception ex)
            {
                return new AIMenuResponse
                {
                    Success = false,
                    Message = $"Error generating menu: {ex.Message}"
                };
            }
        }

        private async Task<DailyMealDto> GenerateDailyMenuAsync(
            int dayNumber,
            DateTime date,
            int breakfastCount,
            int lunchCount,
            int dinnerCount,
            int snackCount,
            AIMenuRequest request)
        {
            var dailyMeal = new DailyMealDto
            {
                DayNumber = dayNumber,
                DayName = date.ToString("dddd"),
                Date = date,
                Meals = new List<MealDto>()
            };

            // ========== GENERATE BREAKFAST ==========
            var breakfasts = await SelectMealsForCategoryAsync(
                "Breakfast", request.Goal, request.Budget,
                request.CuisinePreferences, request.Allergies,
                request.IsVegetarian, request.IsVegan, breakfastCount);
            dailyMeal.Meals.AddRange(breakfasts);

            // ========== GENERATE LUNCH ==========
            var lunches = await SelectMealsForCategoryAsync(
                "Lunch", request.Goal, request.Budget,
                request.CuisinePreferences, request.Allergies,
                request.IsVegetarian, request.IsVegan, lunchCount);
            dailyMeal.Meals.AddRange(lunches);

            // ========== GENERATE DINNER ==========
            var dinners = await SelectMealsForCategoryAsync(
                "Dinner", request.Goal, request.Budget,
                request.CuisinePreferences, request.Allergies,
                request.IsVegetarian, request.IsVegan, dinnerCount);
            dailyMeal.Meals.AddRange(dinners);

            // ========== GENERATE SNACKS ==========
            if (snackCount > 0)
            {
                var snacks = await SelectMealsForCategoryAsync(
                    "Snack", request.Goal, request.Budget,
                    request.CuisinePreferences, request.Allergies,
                    request.IsVegetarian, request.IsVegan, snackCount);
                dailyMeal.Meals.AddRange(snacks);
            }

            // ========== CALCULATE DAILY NUTRITION ==========
            dailyMeal.DailyCalories = dailyMeal.Meals.Sum(m => m.CaloriesPerServing);
            dailyMeal.DailyProtein = dailyMeal.Meals.Sum(m => m.ProteinG);
            dailyMeal.DailyCarbs = dailyMeal.Meals.Sum(m => m.CarbsG);
            dailyMeal.DailyFat = dailyMeal.Meals.Sum(m => m.FatG);

            return dailyMeal;
        }

        public async Task<List<MealDto>> SelectMealsForCategoryAsync(
            string category,
            string goal,
            string budget,
            string cuisinePreferences,
            string allergies,
            bool isVegetarian,
            bool isVegan,
            int mealsNeeded)
        {
            // ========== GET FILTERED MEALS FROM REPOSITORY ==========
            var availableMeals = await _mealRepository.GetMealsForAISelectionAsync(
                category, goal, budget, cuisinePreferences, allergies, isVegetarian, isVegan);

            var mealList = availableMeals.ToList();

            // ========== FALLBACK IF NO MEALS MATCH CRITERIA ==========
            if (!mealList.Any())
            {
                // Relax criteria and try again
                mealList = (await _mealRepository.GetMealsByCategoryAsync(category)).ToList();
            }

            // ========== FAKE AI SELECTION LOGIC ==========
            var selectedMeals = new List<MealDto>();
            var usedMealIds = new HashSet<int>();

            for (int i = 0; i < mealsNeeded; i++)
            {
                Meal selectedMeal;

                if (goal == "WeightLoss")
                {
                    // Prefer low-calorie meals
                    selectedMeal = mealList
                        .Where(m => !usedMealIds.Contains(m.Id))
                        .OrderBy(m => m.CaloriesPerServing)
                        .FirstOrDefault();
                }
                else if (goal == "MuscleGain")
                {
                    // Prefer high-protein meals
                    selectedMeal = mealList
                        .Where(m => !usedMealIds.Contains(m.Id))
                        .OrderByDescending(m => m.ProteinG)
                        .FirstOrDefault();
                }
                else
                {
                    // Random selection for "Maintain"
                    var availableForSelection = mealList
                        .Where(m => !usedMealIds.Contains(m.Id))
                        .ToList();

                    if (availableForSelection.Any())
                    {
                        var randomIndex = new Random().Next(availableForSelection.Count);
                        selectedMeal = availableForSelection[randomIndex];
                    }
                    else
                    {
                        selectedMeal = null;
                    }
                }

                // ========== ADD TO SELECTION ==========
                if (selectedMeal != null)
                {
                    usedMealIds.Add(selectedMeal.Id);
                    selectedMeals.Add(ConvertToMealDto(selectedMeal, goal));
                }
                else
                {
                    // If no more unique meals, reuse meals
                    if (mealList.Any())
                    {
                        var fallbackMeal = mealList[new Random().Next(mealList.Count)];
                        selectedMeals.Add(ConvertToMealDto(fallbackMeal, goal));
                    }
                }
            }

            return selectedMeals;
        }

        public async Task<MealDto> GetRandomMealForCategoryAsync(
            string category,
            List<int> excludeMealIds = null)
        {
            var meals = await _mealRepository.GetRandomMealsAsync(10, category);
            var mealList = meals.ToList();

            if (excludeMealIds != null)
            {
                mealList = mealList.Where(m => !excludeMealIds.Contains(m.Id)).ToList();
            }

            if (!mealList.Any())
            {
                return null;
            }

            var randomMeal = mealList[new Random().Next(mealList.Count)];
            return ConvertToMealDto(randomMeal, "Maintain");
        }

        public bool ValidateNutritionGoals(WeeklyMenuDto menu, AIMenuRequest request)
        {
            var avgCalories = menu.Days.Average(d => d.DailyCalories);
            var calorieVariance = Math.Abs(avgCalories - request.TargetCalories) / request.TargetCalories;

            // Accept if within 20% of target
            return calorieVariance <= 0.20;
        }

        public MenuNutritionSummary CalculateNutritionSummary(WeeklyMenuDto menu, AIMenuRequest request)
        {
            var summary = new MenuNutritionSummary();

            summary.AverageCaloriesPerDay = menu.Days.Average(d => d.DailyCalories);
            summary.AverageProteinPerDay = menu.Days.Average(d => d.DailyProtein);
            summary.AverageCarbsPerDay = menu.Days.Average(d => d.DailyCarbs);
            summary.AverageFatPerDay = menu.Days.Average(d => d.DailyFat);

            summary.CalorieVariance = Math.Abs(summary.AverageCaloriesPerDay - request.TargetCalories) / request.TargetCalories * 100;
            summary.MeetsNutritionGoals = ValidateNutritionGoals(menu, request);

            // Add nutrition notes
            if (summary.CalorieVariance > 20)
            {
                summary.NutritionNotes.Add($"Calorie variance: {summary.CalorieVariance:F1}% from target");
            }

            if (summary.AverageProteinPerDay < request.TargetProteinG * 0.8)
            {
                summary.NutritionNotes.Add("Protein intake may be below target");
            }

            return summary;
        }

        public async Task<WeeklyMenuDto> OptimizeMenuForGoalsAsync(WeeklyMenuDto menu, AIMenuRequest request)
        {
            // Simple optimization: replace some meals if calories are too high/low
            var avgCalories = menu.Days.Average(d => d.DailyCalories);

            if (avgCalories > request.TargetCalories * 1.2) // Too high
            {
                // Replace some high-calorie meals with lower-calorie options
                foreach (var day in menu.Days.Take(3)) // Optimize first 3 days
                {
                    var highCalorieMeal = day.Meals.OrderByDescending(m => m.CaloriesPerServing).FirstOrDefault();
                    if (highCalorieMeal != null)
                    {
                        var replacement = await GetRandomMealForCategoryAsync(
                            highCalorieMeal.Category,
                            new List<int> { highCalorieMeal.MealId });

                        if (replacement != null && replacement.CaloriesPerServing < highCalorieMeal.CaloriesPerServing)
                        {
                            var index = day.Meals.IndexOf(highCalorieMeal);
                            day.Meals[index] = replacement;
                        }
                    }
                }
            }

            // Recalculate daily nutrition after optimization
            foreach (var day in menu.Days)
            {
                day.DailyCalories = day.Meals.Sum(m => m.CaloriesPerServing);
                day.DailyProtein = day.Meals.Sum(m => m.ProteinG);
                day.DailyCarbs = day.Meals.Sum(m => m.CarbsG);
                day.DailyFat = day.Meals.Sum(m => m.FatG);
            }

            return menu;
        }

        // ==================== HELPER METHODS ====================

        private (int breakfast, int lunch, int dinner, int snack) CalculateMealsPerDay(int mealsPerDay, string planType)
        {
            if (planType == "Premium")
            {
                // Premium: 5 meals per day (3 main + 2 snacks)
                return (1, 1, 1, 2);
            }
            else
            {
                // Basic: 3 meals per day
                return (1, 1, 1, 0);
            }
        }

        private MealDto ConvertToMealDto(Meal meal, string goal)
        {
            var dto = new MealDto
            {
                MealId = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                Category = meal.Category,
                Cuisine = meal.Cuisine,
                CaloriesPerServing = meal.CaloriesPerServing,
                ProteinG = meal.ProteinG,
                CarbsG = meal.CarbsG,
                FatG = meal.FatG,
                FiberG = meal.FiberG,
                PrepTimeMinutes = meal.PrepTimeMinutes,
                DifficultyLevel = meal.DifficultyLevel,
                ImagePath = meal.ImagePath,
                Tags = meal.Tags
            };

            // Add AI selection reasoning
            dto.SelectionReason = goal switch
            {
                "WeightLoss" => $"Low calorie ({meal.CaloriesPerServing:F0} cal) for weight management",
                "MuscleGain" => $"High protein ({meal.ProteinG:F0}g) for muscle building",
                _ => $"Balanced nutrition for maintenance"
            };

            return dto;
        }

        private double CalculateEstimatedCost(WeeklyMenuDto menu, string budget)
        {
            var baseCostPerMeal = budget switch
            {
                "Low" => 3.5,
                "Medium" => 5.0,
                "High" => 7.5,
                _ => 5.0
            };

            return menu.TotalMeals * baseCostPerMeal;
        }
    }
}
