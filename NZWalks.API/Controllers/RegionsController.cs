using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepo _regionRepo;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepo regionRepo)
        {
            this.dbContext = dbContext;
            _regionRepo = regionRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepo.GetAllAsync();

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
            var region = await _regionRepo.GetByIdAsync(id);

            
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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            regionDomain = await _regionRepo.CreateAsync(regionDomain);

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
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
            {
                if (updateRegionRequestDto == null)
                    return BadRequest("Region data is required.");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var regionDomain = new Region
                {
                    Code = updateRegionRequestDto.Code,
                    Name = updateRegionRequestDto.Name,
                    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                };

                var updatedRegion = await _regionRepo.UpdateAsync(id, regionDomain);
                if (updatedRegion == null)
                    return NotFound($"Region with Id {id} not found.");

                var regionDto = new RegionsDTO
                {
                    Id = updatedRegion.Id,
                    Code = updatedRegion.Code,
                    Name = updatedRegion.Name,
                    RegionImageUrl = updatedRegion.RegionImageUrl
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