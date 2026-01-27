using System.ComponentModel.DataAnnotations;

namespace MealPrepService.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";
    }
}
