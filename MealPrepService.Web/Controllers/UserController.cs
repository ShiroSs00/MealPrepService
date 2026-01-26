using MealPrepService.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using MealPrepService.Web.Models;

namespace MealPrepService.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // =====================
        // REGISTER
        // =====================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //  VALIDATE MODEL
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.Register(
                model.Name,
                model.Email,
                model.Password
            );

            return RedirectToAction("Profile", new { userId = user.Id });
        }

        [HttpGet]
        public IActionResult Profile(int userId)
        {
            // 1️⃣ LOAD USER (BODY INFO)
            var user = _userService.GetUserById(userId);
            if (user == null)
                return NotFound();

            // 2️⃣ LOAD NUTRITION PROFILE (NẾU CÓ)
            var profile = _userService.GetActiveNutritionProfile(userId);

            // 3️⃣ BIND DATA VÀO VIEWMODEL
            var model = new ProfileViewModel
            {
                UserId = userId,

                // ===== BODY INFO (Users table) =====
                HeightCm = user.HeightCm,
                WeightKg = user.WeightKg,
                BirthDate = user.BirthDate,
                Gender = user.Gender,

                // ===== NUTRITION PROFILE =====
                ActivityLevel = profile?.ActivityLevel ?? "",
                Goal = profile?.Goal ?? "",
                Allergies = profile?.Allergies ?? "",
                CuisinePreferences = profile?.CuisinePreferences ?? "",
                Budget = profile?.Budget ?? "Low",
                MealsPerDay = profile?.MealsPerDay ?? 3
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //  UPDATE USER TRƯỚC
            _userService.UpdateBasicProfile(
                model.UserId,
                model.HeightCm,
                model.WeightKg,
                model.BirthDate,
                model.Gender
            );

            //  SAU ĐÓ MỚI TẠO NUTRITION PROFILE
            _userService.CreateNutritionProfile(
                model.UserId,
                model.ActivityLevel,
                model.Goal,
                model.Allergies,
                model.CuisinePreferences,
                model.Budget,
                model.MealsPerDay
            );

            return RedirectToAction("Dashboard", new { userId = model.UserId });
        }


        // =====================
        // DASHBOARD
        // =====================
        [HttpGet]
        public IActionResult Dashboard(int userId)
        {
            var profile = _userService.GetActiveNutritionProfile(userId);

            //  TRÁNH CRASH KHI CHƯA CÓ PROFILE
            if (profile == null)
                return RedirectToAction("Profile", new { userId });

            return View(profile);
        }
    }
}
