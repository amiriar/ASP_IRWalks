using ASP_CORE_API.CustomActionFilters;
using ASP_CORE_API.Data;
using ASP_CORE_API.Models.Domain;
using ASP_CORE_API.Models.Dtos;
using ASP_CORE_API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ASP_CORE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Getall was called");
            var regions = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            logger.LogInformation($"finished get all {JsonSerializer.Serialize(regionsDto)}");

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regions = await regionRepository.GetAllAsync();

            if (regions == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regions));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPatch]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
