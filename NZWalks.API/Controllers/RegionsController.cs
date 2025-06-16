using AutoMapper;
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
        private readonly IRegionRepo _regionRepo;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepo regionRepo,IMapper mapper)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepo.GetAllAsync();
            
            var regionsDto = _mapper.Map<List<RegionsDTO>>(regionsDomain);
            
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
            return Ok(_mapper.Map<RegionsDTO>(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            regionDomain = await _regionRepo.CreateAsync(regionDomain);

            var regionDto = _mapper.Map<RegionsDTO>(regionDomain);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
           
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomain = _mapper.Map<Region>(updateRegionRequestDto);

            var updatedRegion = await _regionRepo.UpdateAsync(id, regionDomain);
            if (updatedRegion == null)
                return NotFound($"Region with Id {id} not found.");
                
            return Ok(_mapper.Map<RegionsDTO>(updatedRegion));
        }
        

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepo.DeleteAsync(id);
            
            if (regionDomain == null)
            {
                return NotFound($"Region with Id {id} not found.");
            }
            
            return Ok(_mapper.Map<RegionsDTO>(regionDomain));
        }
        
    }
}