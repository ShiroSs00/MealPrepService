using Microsoft.AspNetCore.Mvc;
using MealPrepService.BLL.Services;
using MealPrepService.BLL.Models;
using MealPrepService.Web.Models;



namespace MealPrepService.Web.Controllers
{
    public class AIMenuController : Controller
    {
        private readonly IAIMenuService _aiMenuService;
        private readonly UserService _userService;

        public AIMenuController(IAIMenuService aiMenuService, UserService userService)
        {
            _aiMenuService = aiMenuService;
            _userService = userService;
        }

        // ========== PLAN SELECTION PAGE ==========
        [HttpGet]
        public IActionResult PlanSelection(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            // Get user's nutrition profile for pre-filling form
            var profile = _userService.GetActiveNutritionProfile(userId);

            if (profile == null)
            {
                return RedirectToAction("Profile", "User", new { userId });
            }

            var model = new PlanSelectionViewModel
            {
                UserId = userId,
                NutritionProfileId = profile.Id,
                Goal = profile.Goal,
                Budget = profile.Budget,
                CuisinePreferences = profile.CuisinePreferences ?? "",
                Allergies = profile.Allergies ?? "",
                IsVegetarian = profile.CuisinePreferences?.Contains("Vegetarian") ?? false,
                IsVegan = profile.CuisinePreferences?.Contains("Vegan") ?? false,
                MealsPerDay = profile.MealsPerDay,
                WeekStartDate = GetNextMonday(),
                PlanType = "Basic" // Default
            };

            return View(model);
        }

        // ========== GENERATE AI MENU ==========
        [HttpPost]
        public async Task<IActionResult> GenerateMenu(PlanSelectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("PlanSelection", model);
            }

            try
            {
                // Get nutrition profile for calorie targets
                var profile = _userService.GetActiveNutritionProfile(model.UserId);

                if (profile == null)
                {
                    ModelState.AddModelError("", "Nutrition profile not found");
                    return View("PlanSelection", model);
                }

                // Create AI menu request
                var aiRequest = new AIMenuRequest
                {
                    UserId = model.UserId,
                    NutritionProfileId = profile.Id,
                    Goal = profile.Goal,
                    TargetCalories = profile.TargetCalories,
                    TargetProteinG = profile.TargetProteinG,
                    TargetCarbsG = profile.TargetCarbsG,
                    TargetFatG = profile.TargetFatG,
                    MealsPerDay = profile.MealsPerDay,
                    Budget = profile.Budget,
                    CuisinePreferences = profile.CuisinePreferences ?? "",
                    Allergies = profile.Allergies ?? "",
                    IsVegetarian = model.IsVegetarian,
                    IsVegan = model.IsVegan,
                    WeekStartDate = model.WeekStartDate,
                    WeekNumber = GetWeekNumber(model.WeekStartDate),
                    PlanType = model.PlanType,
                    TotalMealsPerWeek = model.PlanType == "Premium" ? 35 : 21
                };

                // Generate menu using AI service
                var aiResponse = await _aiMenuService.GenerateWeeklyMenuAsync(aiRequest);

                if (!aiResponse.Success)
                {
                    ModelState.AddModelError("", aiResponse.Message);
                    return View("PlanSelection", model);
                }

                // Create menu review model
                var menuReviewModel = new MenuReviewViewModel
                {
                    UserId = model.UserId,
                    PlanType = model.PlanType,
                    WeekStartDate = model.WeekStartDate,
                    WeeklyMenu = aiResponse.WeeklyMenu,
                    NutritionSummary = aiResponse.NutritionSummary,
                    GeneratedAt = aiResponse.GeneratedAt
                };

                return View("MenuReview", menuReviewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error generating menu: {ex.Message}");
                return View("PlanSelection", model);
            }
        }

        // ========== MENU REVIEW PAGE ==========
        [HttpGet]
        public IActionResult MenuReview(int? userId)
        {
            // Redirect to plan selection if accessed directly
            if (userId.HasValue)
            {
                return RedirectToAction("PlanSelection", new { userId = userId.Value });
            }
            return RedirectToAction("Index", "Home"); // Fallback
        }

        // ========== CONFIRM MENU AND CREATE ORDER ==========
        [HttpPost]
        public IActionResult ConfirmMenu(MenuReviewViewModel model) // ✅ Removed async - không cần thiết
        {
            try
            {
                // TODO: Create Order entity and save to database
                // For now, just show success message

                TempData["SuccessMessage"] = "Menu confirmed successfully! Your meal plan has been created.";

                // ✅ Fallback to Profile if Dashboard doesn't exist
                return RedirectToAction("Profile", "User", new { userId = model.UserId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error confirming menu: {ex.Message}";
                return View(model);
            }
        }

        // ========== REGENERATE MENU ==========
        [HttpPost]
        public async Task<IActionResult> RegenerateMenu(MenuReviewViewModel model)
        {
            try
            {
                // ✅ Get fresh profile data for regeneration
                var profile = _userService.GetActiveNutritionProfile(model.UserId);
                if (profile == null)
                {
                    TempData["ErrorMessage"] = "Nutrition profile not found";
                    return View("MenuReview", model);
                }

                // Convert back to plan selection model and regenerate
                var planModel = new PlanSelectionViewModel
                {
                    UserId = model.UserId,
                    NutritionProfileId = profile.Id,
                    Goal = profile.Goal,
                    Budget = profile.Budget,
                    PlanType = model.PlanType,
                    WeekStartDate = model.WeekStartDate,
                    CuisinePreferences = profile.CuisinePreferences ?? "",
                    Allergies = profile.Allergies ?? "",
                    MealsPerDay = profile.MealsPerDay,
                    IsVegetarian = false, // Could be extracted from menu data
                    IsVegan = false
                };

                return await GenerateMenu(planModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error regenerating menu: {ex.Message}";
                return View("MenuReview", model);
            }
        }

        // ========== HELPER METHODS ==========
        private DateTime GetNextMonday()
        {
            var today = DateTime.Today;
            var daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;

            // ✅ Sửa lỗi: Nếu hôm nay là Monday, lấy Monday tuần sau
            if (daysUntilMonday == 0)
                daysUntilMonday = 7;

            return today.AddDays(daysUntilMonday);
        }

        private int GetWeekNumber(DateTime date)
        {
            try
            {
                var dayOfYear = date.DayOfYear;
                return Math.Max(1, (dayOfYear - 1) / 7 + 1); // ✅ Ensure minimum week 1
            }
            catch
            {
                return 1; // ✅ Fallback
            }
        }
    }
}


