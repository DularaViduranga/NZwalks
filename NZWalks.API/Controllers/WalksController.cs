using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalksDto;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;

        public WalksController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addWalks)
        {
            var walkDomain = _mapper.Map<Walk>(addWalks);
        }
    }
}
