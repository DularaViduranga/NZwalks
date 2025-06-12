using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute]Guid id)
        {
            var region = dbContext.Regions.Find(id);

            
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
        public IActionResult create()
        {
            
        }
        
    }
}