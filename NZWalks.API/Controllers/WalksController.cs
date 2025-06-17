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

        public WalksController(IMapper mapper, IWalkRepo walkRepo)
        {
            _mapper = mapper;
            _walkRepo = walkRepo;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalks)
        {
            var walkDomain = _mapper.Map<Walk>(addWalks);
            
            await _walkRepo.CreateAsync(walkDomain);
            
            var walkDto = _mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var walksDomain = await _walkRepo.GetAllAsync();
            
            return Ok(_mapper.Map<List<WalkDTO>>(walksDomain));
        }
        
        
        
    }
}
