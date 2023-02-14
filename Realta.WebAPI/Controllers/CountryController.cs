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
    public class CountryController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public CountryController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public IActionResult FindAllCountry()
        {
            try
            {
                var country = _repositoryManager.CountryRepository.FindAllCountry().ToList();
                return Ok(country);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllCountry)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}", Name = "GetCountry")]
        public IActionResult FindCountryById(int id)
        {
            var country = _repositoryManager.CountryRepository.FindCountryById(id);
            if (country == null)
            {
                _logger.LogError("Country object sent from client is null");
                return BadRequest("Country object not found");
            }
            var CountryDto = new CountryDto
            {
                country_id = country.country_id,
                country_name = country.country_name,
                country_region_id = country.country_region_id
            };
            return Ok(CountryDto);
        }

        // POST api/<CountryController>
        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryDto countryDto)
        {
            if (countryDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Region object is null");
            }
            var country = new Country()
            {
                country_id = countryDto.country_id,
                country_name = countryDto.country_name,
                country_region_id = countryDto.country_region_id
            };

            _repositoryManager.CountryRepository.Insert(country);
            countryDto.country_id = country.country_id;
            return CreatedAtRoute("GetCountry", new { id = countryDto.country_id }, countryDto);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, CountryDto countryDto)
        {
            if (countryDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Country object is null");
            }

            var country = new Country()
            {
                country_id = id,
                country_name = countryDto.country_name,
                country_region_id = countryDto.country_region_id
            };
            _repositoryManager.CountryRepository.Edit(country);
            return CreatedAtRoute("GetCountry",new { id = countryDto.country_id },new CountryDto { country_id = id, country_name = country.country_name, country_region_id = country.country_region_id});
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Country object is null");
            }

            var country = _repositoryManager.CountryRepository.FindCountryById(id);
            if (country == null)
            {
                _logger.LogError($"Country with {id} not found");
                return NotFound();
            }
            _repositoryManager.CountryRepository.Remove(country);
            return Ok("Data has been removed");
        }
    }
}
