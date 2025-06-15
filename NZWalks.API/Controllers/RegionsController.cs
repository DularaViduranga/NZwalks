using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await dbContext.Regions.ToListAsync();

            var regionsDto = new List<RegionsDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionsDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }
            
            return Ok(regionsDto); 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var region = await dbContext.Regions.FindAsync(id);

            
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionsDTO();
            regionDto.Id = region.Id;
            regionDto.Code = region.Code;
            regionDto.Name = region.Name;
            regionDto.RegionImageUrl = region.RegionImageUrl;
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {

            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            await dbContext.Regions.AddAsync(regionDomain);
            await dbContext.SaveChangesAsync();

            var regionDto = new RegionsDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = await dbContext.Regions.FindAsync(id);
            if (regionDomain == null)
            {
                return NotFound($"Region with Id {id} not found.");
            }
            
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            
            var regionDto = new RegionsDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await dbContext.Regions.FindAsync(id);
            if (regionDomain == null)
            {
                return NotFound($"Region with Id {id} not found.");
            }
            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
        
    }
}