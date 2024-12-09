using ASP_CORE_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_CORE_API.Data
{
    public class IRWalksDbContext: DbContext
    {
        public IRWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
