using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class UpdateWalkDto
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name field must not exceed 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [MaxLength(100, ErrorMessage = "The Description field must not exceed 100 characters.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "The LengthInKm field is required.")]
        [Range(0.1, 10000, ErrorMessage = "The Length in Km must be between 0.1 and 10,000.")]
        public double LengthInKm { get; set; }

        //[Url(ErrorMessage = "The WalkImageUrl must be a valid URL.")]
        public string? WalkImageUrl { get; set; }

        [Required(ErrorMessage = "The DifficultyId field is required.")]
        public Guid DifficultyId { get; set; }

        [Required(ErrorMessage = "The RegionId field is required.")]
        public Guid RegionId { get; set; }
    }

}
