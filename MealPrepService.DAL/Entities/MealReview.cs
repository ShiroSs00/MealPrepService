using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPrepService.DAL.Entities
{
    [Table("MealReviews")]
    public class MealReview
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Meal Info
        [Required, MaxLength(200)]
        public string MealName { get; set; } = ""; 

        public int DayNumber { get; set; } 

        [MaxLength(20)]
        public string MealType { get; set; } = ""; // Breakfast, Lunch, Dinner, Snack

        // Review
        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 stars

        [MaxLength(1000)]
        public string Comment { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}