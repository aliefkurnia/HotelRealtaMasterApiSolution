using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
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
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryRegionId = country.CountryRegionId
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
                CountryId = countryDto.CountryId,
                CountryName = countryDto.CountryName,
                CountryRegionId = countryDto.CountryRegionId
            };

            _repositoryManager.CountryRepository.Insert(country);
            countryDto.CountryId = country.CountryId;
            return CreatedAtRoute("GetCountry", new { id = countryDto.CountryId }, countryDto);
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
                CountryId = id,
                CountryName = countryDto.CountryName,
                CountryRegionId = countryDto.CountryRegionId
            };
            _repositoryManager.CountryRepository.Edit(country);
            return CreatedAtRoute("GetCountry",new { id = countryDto.CountryId },new CountryDto { CountryId = id, CountryName = country.CountryName, CountryRegionId = country.CountryRegionId});
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

        [HttpGet("pageList")]
        public async Task<IActionResult> GetCountryPageList([FromQuery] CountryParameters countryParameters)
        {
            var country= await _repositoryManager.CountryRepository.GetCountryPageList(countryParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(country.MetaData));
            return Ok(country);
        }
    }
}
