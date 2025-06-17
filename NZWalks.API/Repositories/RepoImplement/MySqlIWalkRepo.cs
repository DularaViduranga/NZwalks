using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Repositories;

public class MySqlIWalkRepo:IWalkRepo
{
    private readonly NZWalksDbContext _dbContext;

    public MySqlIWalkRepo(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _dbContext.Walks.Include(x =>x.Difficulty).Include(x =>x.Region).ToListAsync();
    }
}