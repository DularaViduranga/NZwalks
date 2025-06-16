using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext: DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440004"),
                Name = "Hard"
            }
        };

        modelBuilder.Entity<Difficulty>().HasData(difficulties);
        
        var regions = new List<Region>()
        {
            new Region()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000"),
                Name = "North Island",
                Code = "NI",
                RegionImageUrl = "www.example.com/north-island.jpg",
            },
            new Region()
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                Name = "South Island",
                Code = "SI",
                RegionImageUrl = "www.example.com/south-island.jpg",
            }
        };
        
        modelBuilder.Entity<Region>().HasData(regions);
    }
}