using ASP_CORE_API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class AddWalkDto
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Description { get; set; }

        [Required]
        public double LengthInKm { get; set; } // Removed [MaxLength] because it doesn't apply to numeric types

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }

}
