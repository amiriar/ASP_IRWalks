using ASP_CORE_API.Data;
using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_CORE_API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly IRWalksDbContext dbContext;
        public SQLWalkRepository(IRWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRWalksDbContext DbContext { get; }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllWalksAsync(
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null, 
            bool? isAscending = true, 
            int page = 1, 
            int pageSize = 25
            )
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Apply filtering logic
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.Trim().ToLower();

                switch (filterOn)
                {
                    case "name":
                        walks = walks.Where(x => x.Name.Contains(filterQuery));
                        break;
                    case "description":
                        walks = walks.Where(x => x.Description.Contains(filterQuery));
                        break;
                    case "regionid":
                        if (Guid.TryParse(filterQuery, out var regionId))
                        {
                            walks = walks.Where(x => x.RegionId == regionId);
                        }
                        break;
                    default:
                        // No action for unrecognized filters
                        break;
                }
            }

            // Apply sorting logic
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.Trim().ToLower();

                // Check if sorting order is ascending or descending
                bool isSortAscending = isAscending ?? true;

                walks = sortBy switch
                {
                    "name" => isSortAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name),
                    "description" => isSortAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description),
                    "lengthinkm" => isSortAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm),
                    _ => walks // No sorting if sortBy doesn't match
                };
            }

            // Apply pagination logic
            var skipResults = page != 0 ? (page - 1) * pageSize : 0;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync([FromRoute] Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, UpdateWalkDto updateWalkDto)
        {
            var existWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existWalk == null)
            {
                return null;
            }

            existWalk.Name = updateWalkDto.Name;
            existWalk.Description = updateWalkDto.Description;
            existWalk.WalkImageUrl = updateWalkDto.WalkImageUrl;
            existWalk.LengthInKm = updateWalkDto.LengthInKm;
            existWalk.RegionId = updateWalkDto.RegionId;
            existWalk.DifficultyId = updateWalkDto.DifficultyId;

            await dbContext.SaveChangesAsync();

            return existWalk;
        }
    }
}
