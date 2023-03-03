using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public ProvincesController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<ProvincesController>
        [HttpGet]
        public IActionResult FindAllProvinces()
        {
            try
            {
                var provinces = _repositoryManager.ProvincesRepository.FindAllProvinces().ToList();
                return Ok(provinces);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllProvinces)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<ProvincesController>/5
        [HttpGet("{id}", Name = "GetProvinces")]
        public IActionResult FindProvincesById(int id)
        {
            var provinces = _repositoryManager.ProvincesRepository.FindProvincesById(id);
            if (provinces == null)
            {
                _logger.LogError("Provinces object sent from client is null");
                return BadRequest("Provinces object not found");
            }
            var provincessDto = new ProvincesDto
            {
                ProvId = provinces.ProvId,
                ProvName = provinces.ProvName,
                ProvCountryId = provinces.ProvCountryId,
            };
            return Ok(provincessDto);
        }

        // POST api/<ProvincesController>
        [HttpPost]
        public IActionResult CreateProvinces([FromBody] ProvincesDto provincesDto)
        {
            if (provincesDto == null)
            {
                _logger.LogError("provincesDto object sent from client is null");
                return BadRequest("Provinces object is null");
            }

            var provinces = new Provinces()
            {
                ProvId = provincesDto.ProvId,
                ProvName = provincesDto.ProvName,
                ProvCountryId = provincesDto.ProvCountryId
            };

            //execute method Insert
            _repositoryManager.ProvincesRepository.Insert(provinces);
            provincesDto.ProvId = provinces.ProvId; 
            return CreatedAtRoute("GetProvinces", new { id = provincesDto.ProvId }, provincesDto);

            //return CreatedAtRoute("GetRegion", new { id = provincesDto.region_code }, new provincesDto { region_code = provincesDto.region_code, region_name = provinces.region_name });

            //var res = _repositoryManager.ProvincesRepository.FindLastProvinces().ToList();
            //return Ok(res);
        }

        // PUT api/<ProvincesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProvinces(int id, [FromBody] ProvincesDto provincesDto)
        {
            if (provincesDto == null)
            {
                _logger.LogError("provincesDto object sent from client is null");
                return BadRequest("provinces object is null");
            }

            var provinces = new Provinces
            {
                ProvId = id,
                ProvName = provincesDto.ProvName,
                ProvCountryId = provincesDto.ProvCountryId
            };

            _repositoryManager.ProvincesRepository.Edit(provinces);
            return CreatedAtRoute("GetProvinces", new { id = provincesDto.ProvId }, new ProvincesDto { ProvId= id, ProvName= provinces.ProvName, ProvCountryId= provincesDto.ProvCountryId});
        }

        // DELETE api/<ProvincesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("ProvincesDto object sent from client is null");
                return BadRequest("Provinces object is null");
            }

            var provinces = _repositoryManager.ProvincesRepository.FindProvincesById(id);
            if (provinces == null)
            {
                _logger.LogError($"Provinces with {id} not found");
                return NotFound();
            }
            _repositoryManager.ProvincesRepository.Remove(provinces);
            return Ok("Data has been removed");
        }
    }
}
