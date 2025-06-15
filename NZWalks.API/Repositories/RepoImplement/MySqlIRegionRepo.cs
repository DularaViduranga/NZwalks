using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class MySqlRegionRepo : IRegionRepo
    {
        private readonly NZWalksDbContext nzWalksDbContext;

        public MySqlRegionRepo(NZWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await nzWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await nzWalksDbContext.Regions.AddAsync(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
                return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await nzWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
                return false;

            nzWalksDbContext.Regions.Remove(region);
            await nzWalksDbContext.SaveChangesAsync();
            return true;
        }
    }
}