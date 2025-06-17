using NZWalks.API.Models.DTO.Difficulty;

namespace NZWalks.API.Models.DTO.WalksDto;

public class WalkDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double LengthInKm { get; set; }
    
    public string? WalkImageUrl { get; set; }

    public Guid DifficultyId { get; set; }

    public Guid RegionId { get; set; }

    // Add navigation properties
    public DifficultyDTO Difficulty { get; set; }
    public RegionsDTO Region { get; set; }
}