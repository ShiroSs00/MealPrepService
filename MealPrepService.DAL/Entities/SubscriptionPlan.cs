using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPrepService.DAL.Entities
{
    [Table("SubscriptionPlans")]
    public class SubscriptionPlan
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = ""; // Basic, Premium

        [MaxLength(500)]
        public string Description { get; set; } = "";

        public int MealsPerWeek { get; set; } // 21 meals (Basic) or 35 meals (Premium)

        public decimal PricePerWeek { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public ICollection<Order> Orders { get; set; } = [];
    }
}