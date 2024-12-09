using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public required string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
