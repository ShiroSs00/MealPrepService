using System.ComponentModel.DataAnnotations;

namespace MealPrepService.Web.Models
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }

        // BODY INFO
        [Range(50, 250, ErrorMessage = "Height must be between 50–250 cm")]
        public double HeightCm { get; set; }

        [Range(20, 300, ErrorMessage = "Weight must be between 20–300 kg")]
        public double WeightKg { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; } = "";

        [Required]
        public string ActivityLevel { get; set; } = "";

        [Required]
        public string Goal { get; set; } = "";

        public string Allergies { get; set; } = "";
        public string CuisinePreferences { get; set; } = "";
        public string Budget { get; set; } = "";
        public int MealsPerDay { get; set; } = 3;
    }
}
