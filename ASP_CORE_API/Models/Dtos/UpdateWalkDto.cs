namespace ASP_CORE_API.Models.Dtos
{
    public class UpdateWalkDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
