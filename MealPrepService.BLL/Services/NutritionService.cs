using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPrepService.BLL.Services
{
    public class NutritionService
    {
        public double CalculateBMR(
            double weightKg,
            double heightCm,
            int age,
            string gender)
        {
            if (gender == "M")
                return 10 * weightKg + 6.25 * heightCm - 5 * age + 5;

            return 10 * weightKg + 6.25 * heightCm - 5 * age - 161;
        }

        public double CalculateTDEE(double bmr, string activityLevel)
        {
            double factor = activityLevel switch
            {
                "Sedentary" => 1.2,
                "Light" => 1.375,
                "Moderate" => 1.55,
                "Active" => 1.725,
                "VeryActive" => 1.9,
                _ => 1.2
            };

            return bmr * factor;
        }

        public double AdjustCaloriesByGoal(double tdee, string goal)
        {
            return goal switch
            {
                "WeightLoss" => tdee - 500,
                "MuscleGain" => tdee + 300,
                _ => tdee
            };
        }

        public (double proteinG, double carbsG, double fatG)
    CalculateMacros(double targetCalories, string goal)
        {
            double proteinRatio, carbsRatio, fatRatio;

            switch (goal)
            {
                case "WeightLoss":
                    proteinRatio = 0.30;
                    carbsRatio = 0.40;
                    fatRatio = 0.30;
                    break;

                case "MuscleGain":
                    proteinRatio = 0.30;
                    carbsRatio = 0.50;
                    fatRatio = 0.20;
                    break;

                default: // Maintain
                    proteinRatio = 0.25;
                    carbsRatio = 0.50;
                    fatRatio = 0.25;
                    break;
            }

            var proteinG = (targetCalories * proteinRatio) / 4;
            var carbsG = (targetCalories * carbsRatio) / 4;
            var fatG = (targetCalories * fatRatio) / 9;

            return (proteinG, carbsG, fatG);
        }


    }

}
