using MealPrepService.DAL.Entities;
using System;
using System.Linq;

namespace MealPrepService.BLL.Services
{
    public class UserService
    {
        private readonly MealPrepDbContext _context;
        private readonly NutritionService _nutritionService;

        public UserService(
            MealPrepDbContext context,
            NutritionService nutritionService)
        {
            _context = context;
            _nutritionService = nutritionService;
        }

        // =====================
        // LOGIN USER
        // =====================
        public User Login(string email, string password)
        {
            // ⚠️ WARNING: This is a simple implementation for demo purposes
            // In production, use proper password hashing (BCrypt, Identity, etc.)
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            return user;
        }

        // =====================
        // REGISTER USER
        // =====================
        public User Register(string name, string email, string passwordHash)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Email already registered");
            }

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public NutritionProfile CreateNutritionProfile(
    int userId,
    string activityLevel,
    string goal,
    string allergies,
    string cuisine,
    string budget,
    int mealsPerDay)
        {
            var user = _context.Users.Find(userId)
                ?? throw new Exception("User not found");

            //  Không cho tạo nếu chưa có body info
            if (user.HeightCm <= 0 || user.WeightKg <= 0 || user.BirthDate == default)
            {
                throw new Exception("User basic profile is incomplete");
            }

            // =============================
            //  TẮT TOÀN BỘ PROFILE CŨ
            // =============================
            var activeProfiles = _context.NutritionProfiles
                .Where(p => p.UserId == userId && p.IsActive)
                .ToList();

            foreach (var p in activeProfiles)
            {
                p.IsActive = false;
            }

            // =============================
            // TÍNH TOÁN DINH DƯỠNG
            // =============================
            int age = DateTime.Now.Year - user.BirthDate.Year;

            var bmr = _nutritionService.CalculateBMR(
                user.WeightKg,
                user.HeightCm,
                age,
                user.Gender
            );

            var tdee = _nutritionService.CalculateTDEE(bmr, activityLevel);
            var targetCalories = _nutritionService.AdjustCaloriesByGoal(tdee, goal);
            var (proteinG, carbsG, fatG) = _nutritionService.CalculateMacros(targetCalories, goal);
            // =============================
            // TẠO PROFILE MỚI
            // =============================
            var profile = new NutritionProfile
            {
                UserId = userId,
                ActivityLevel = activityLevel,
                Goal = goal,

                BMR = bmr,
                TDEE = tdee,
                TargetCalories = targetCalories,

                //  MACRO NUTRIENTS
                TargetProteinG = Math.Round(proteinG, 1),
                TargetCarbsG = Math.Round(carbsG, 1),
                TargetFatG = Math.Round(fatG, 1),

                Allergies = allergies,
                CuisinePreferences = cuisine,
                Budget = budget,
                MealsPerDay = mealsPerDay,
                IsActive = true,
                CreatedAt = DateTime.Now
            };


            _context.NutritionProfiles.Add(profile);
            _context.SaveChanges();

            return profile;
        }


        // =====================
        // GET ACTIVE PROFILE
        // =====================
        public NutritionProfile GetActiveNutritionProfile(int userId)
        {
            return _context.NutritionProfiles
                .Where(p => p.UserId == userId && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .FirstOrDefault(); 
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void UpdateBasicProfile(
    int userId,
    double height,
    double weight,
    DateTime birthDate,
    string gender)
        {
            var user = _context.Users.Find(userId)
                ?? throw new Exception("User not found");

            user.HeightCm = height;
            user.WeightKg = weight;
            user.BirthDate = birthDate;
            user.Gender = gender;

            _context.SaveChanges();
        }
    }
}
