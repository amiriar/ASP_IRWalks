﻿namespace ASP_CORE_API.Models.Dtos
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
