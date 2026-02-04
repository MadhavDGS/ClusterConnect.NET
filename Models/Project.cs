using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClusterConnect.Models;

public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "PLANNING"; // PLANNING, ACTIVE, COMPLETED, PAUSED

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [MaxLength(500)]
    public string? TechStack { get; set; }

    public int? TeamSize { get; set; }

    [MaxLength(100)]
    public string? Category { get; set; }

    public bool IsPublic { get; set; } = true;
}
