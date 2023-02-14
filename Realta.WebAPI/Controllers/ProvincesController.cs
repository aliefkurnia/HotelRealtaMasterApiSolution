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
                prov_id = provinces.prov_id,
                prov_name = provinces.prov_name,
                prov_country_id = provinces.prov_country_id,
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
                return BadRequest("Region object is null");
            }

            var provinces = new Provinces()
            {
                prov_id = provincesDto.prov_id,
                prov_name = provincesDto.prov_name,
                prov_country_id = provincesDto.prov_country_id
            };

            //execute method Insert
            _repositoryManager.ProvincesRepository.Insert(provinces);
            provincesDto.prov_id = provinces.prov_id; 
            return CreatedAtRoute("GetRegion", new { id = provincesDto.prov_id }, provincesDto);

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
                prov_id = id,
                prov_name = provincesDto.prov_name,
                prov_country_id = provincesDto.prov_country_id
            };

            _repositoryManager.ProvincesRepository.Edit(provinces);
            return CreatedAtRoute("GetProvinces", new { id = provincesDto.prov_id }, new ProvincesDto { prov_id= id, prov_name= provinces.prov_name, prov_country_id= provincesDto.prov_country_id});
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
