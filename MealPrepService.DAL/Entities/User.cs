using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MealPrepService.DAL.Entities;

[Index(nameof(Email), IsUnique = true)]
[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = "";

    [MaxLength(256)]
    public string PasswordHash { get; set; } = "";

    public double HeightCm { get; set; }
    public double WeightKg { get; set; }
    public DateTime BirthDate { get; set; }
    [MaxLength(10)]
    public string Gender { get; set; } = ""; // M/F

    [MaxLength(15)]
    public string Phone { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relationships
    public ICollection<NutritionProfile> NutritionProfiles { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
}
