using ASP_CORE_API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Repositories;
using ASP_CORE_API.CustomActionFilters;

namespace ASP_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkDto addWalkDto)
        {

            var WalkDoaminModel = _mapper.Map<Walk>(addWalkDto);

            await _walkRepository.CreateAsync(WalkDoaminModel);

            return Ok(_mapper.Map<WalkDto>(WalkDoaminModel));

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walksDomainModel = await _walkRepository.GetAllWalksAsync();

            // Map domain models to DTOs
            var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);

            // Return the DTOs wrapped in an Ok() response
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var existWalk = await _walkRepository.GetByIdAsync(id);
            if (existWalk == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(existWalk));
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDto updateWalkDto)
        {

            var walkDomainModel = _mapper.Map<Walk>(updateWalkDto);

            walkDomainModel = await _walkRepository.UpdateAsync(id, updateWalkDto);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _walkRepository.DeleteAsync(id));
        }
    }
}
