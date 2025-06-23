using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalksDto;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepo _walkRepo;
        private readonly ILogger<WalksController> _logger;

        public WalksController(IMapper mapper, IWalkRepo walkRepo, ILogger<WalksController> logger)
        {
            _mapper = mapper;
            _walkRepo = walkRepo;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalks)
        {
            if (ModelState.IsValid)
            {
                var walkDomain = _mapper.Map<Walk>(addWalks);
            
                await _walkRepo.CreateAsync(walkDomain);
            
                var walkDto = _mapper.Map<WalkDTO>(walkDomain);
                return Ok(walkDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            _logger.LogInformation("Getting all walks");
            var walksDomain = await _walkRepo.GetAllAsync();
            _logger.LogInformation($"Found {walksDomain?.Count ?? 0} walks");
            
            if (walksDomain == null)
            {
                _logger.LogWarning("walksDomain is null");
                return Ok(new List<WalkDTO>());
            }
            
            var walksDto = _mapper.Map<List<WalkDTO>>(walksDomain);
            _logger.LogInformation($"Mapped to {walksDto?.Count ?? 0} DTOs");
            
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var walkDomain = await _walkRepo.GetByIdAsync(id);
            if (walkDomain == null)
                return NotFound();
            
            return Ok(_mapper.Map<WalkDTO>(walkDomain));
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDTO updateWalkRequest)
        {
            if (!ModelState.IsValid)
            {
                var walkDomain = _mapper.Map<Walk>(updateWalkRequest);
                walkDomain = await _walkRepo.UpdateAsync(id, walkDomain);

                if (walkDomain == null)
                {
                    return NotFound();
                }    
            
                return Ok(_mapper.Map<WalkDTO>(walkDomain));
            }
            else
            {
                return BadRequest(ModelState);

            }   
            
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedwalk = await _walkRepo.DeleteAsync(id);

            if (deletedwalk == null)
                return NotFound();
            
            return Ok(_mapper.Map<WalkDTO>(deletedwalk));
        }
    }
}
