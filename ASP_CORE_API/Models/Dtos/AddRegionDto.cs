using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class AddRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(100, ErrorMessage = "Code has to be a maximum of 100 characters")]
        public required string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
