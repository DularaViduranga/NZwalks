using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NZWalks.API.Repositories;

public class MySqlIWalkRepo:IWalkRepo
{
    private readonly NZWalksDbContext _dbContext;
    private readonly ILogger<MySqlIWalkRepo> _logger;

    public MySqlIWalkRepo(NZWalksDbContext dbContext, ILogger<MySqlIWalkRepo> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        var walks = await _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .ToListAsync();
        return walks;
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        
        if (existingWalk == null)
            return null;
            
        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;
        
        await _dbContext.SaveChangesAsync();
        
        return existingWalk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var exitingwalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        
        if(exitingwalk == null)
            return null;

        _dbContext.Walks.Remove(exitingwalk);
        await _dbContext.SaveChangesAsync();
        return (exitingwalk);

    }
}