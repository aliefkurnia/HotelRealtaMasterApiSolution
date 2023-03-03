using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public RegionsController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<RegionsController>
        [HttpGet]
        public IActionResult FindAllRegions ()
        {
            try
            {
                var regions = _repositoryManager.RegionRepository.FindAllRegions().ToList();
                return Ok(regions);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllRegions)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<RegionsController>/5
        [HttpGet("{id}", Name = "GetRegion")]
        public IActionResult FindRegionById(int id)
        {
            var region = _repositoryManager.RegionRepository.FindRegionsById(id);
            if (region == null)
            {
                _logger.LogError("Region object sent from client is null");
                return BadRequest("Region object not found");
            }
            var regionsDto = new RegionsDto
            {
                regionCode = region.RegionCode,
                RegionName = region.RegionName
            };
            return Ok(regionsDto);
        }

        // POST api/<RegionsController>
        [HttpPost]
        public IActionResult CreateRegions([FromBody] RegionsDto regionsDto)
        {
            if (regionsDto == null)
            {
                _logger.LogError("RegionsDto object sent from client is null");
                return BadRequest("Region object is null");
            }

            var regions = new Regions()
            {   
                RegionCode = regionsDto.regionCode,
                RegionName = regionsDto.RegionName
            };

            //execute method Insert
            _repositoryManager.RegionRepository.Insert(regions);

            regionsDto.regionCode = regions.RegionCode;
            return CreatedAtRoute("GetRegion", new { id = regionsDto.regionCode}, regionsDto);

        }

        // PUT api/<RegionsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateRegions(int id,RegionsDto regionsDto)
        {
            if (regionsDto == null)
            {
                _logger.LogError("RegionsDto object sent from client is null");
                return BadRequest("Regions object is null");
            }

            var regions = new Regions
            {
                RegionCode = id,
                RegionName = regionsDto.RegionName
            };

            _repositoryManager.RegionRepository.Edit(regions);
            return CreatedAtRoute("GetRegion", new { id = regionsDto.regionCode }, new RegionsDto { regionCode = id, RegionName = regions.RegionName });
        }

        // DELETE api/<RegionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("RegionsDto object sent from client is null");
                return BadRequest("Regions object is null");
            }

            var regions = _repositoryManager.RegionRepository.FindRegionsById(id.Value);
            if (regions == null)
            {
                _logger.LogError($"Region with {id} not found");
                return NotFound();
            }
            _repositoryManager.RegionRepository.Remove(regions);
            return Ok("Data has been removed");

        }
    }
}
