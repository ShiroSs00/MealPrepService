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
        // LOGIN
        // =====================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = _userService.Login(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    return View(model);
                }

                // ✅ Store user info in session (simple auth)
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserEmail", user.Email);

                // Check if user has completed profile
                var profile = _userService.GetActiveNutritionProfile(user.Id);
                
                if (profile == null)
                {
                    // Redirect to complete profile
                    return RedirectToAction("Profile", new { userId = user.Id });
                }

                // Redirect to dashboard if profile exists
                return RedirectToAction("Dashboard", new { userId = user.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Login failed: {ex.Message}");
                return View(model);
            }
        }

        // =====================
        // LOGOUT
        // =====================
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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

            try
            {
                var user = _userService.Register(
                    model.Name,
                    model.Email,
                    model.Password
                );

                // Auto login after register
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserEmail", user.Email);

                return RedirectToAction("Profile", new { userId = user.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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
