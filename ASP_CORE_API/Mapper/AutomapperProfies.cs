using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Models.Dtos;
using AutoMapper;

namespace ASP_CORE_API.Mapper
{
    public class AutomapperProfies: Profile
    {
        public AutomapperProfies()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            CreateMap<AddWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
