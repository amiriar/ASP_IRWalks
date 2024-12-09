using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Models.Dtos;

namespace ASP_CORE_API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int page = 1, int pageSize = 25);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, UpdateWalkDto updateWalkDto);
        Task<Walk> DeleteAsync(Guid id);
    }
}
