using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPrepService.DAL.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int NutritionProfileId { get; set; }

        public int? SubscriptionPlanId { get; set; }

        // Order Info
        [Required, MaxLength(50)]
        public string OrderCode { get; set; } = ""; // MEAL-ORDER-1234 (QR code text)

        public int WeekNumber { get; set; } // Tuần thứ mấy

        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Paid, Preparing, Delivered, Completed

        // Payment
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = ""; // VNPay, Momo, Cash

        [MaxLength(100)]
        public string PaymentTransactionId { get; set; } = "";

        public DateTime? PaidAt { get; set; }

        // Menu JSON from AI
        [MaxLength(4000)]
        public string MenuJsonData { get; set; } = ""; // AI-generated menu as JSON string

        [MaxLength(500)]
        public string MenuPdfPath { get; set; } = ""; // Path to generated PDF

        // Tracking
        public double CompletionPercentage { get; set; } = 0; // % meals reviewed
        public double AverageRating { get; set; } = 0; // Avg rating từ meal reviews

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relationships
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(NutritionProfileId))]
        public NutritionProfile NutritionProfile { get; set; } = null!;

        [ForeignKey(nameof(SubscriptionPlanId))]
        public SubscriptionPlan? SubscriptionPlan { get; set; }

        public ICollection<MealReview> MealReviews { get; set; } = [];
    }
}