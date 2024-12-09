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

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
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
