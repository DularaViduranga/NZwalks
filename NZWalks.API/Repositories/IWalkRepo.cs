using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface IWalkRepo
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllAsync();
}