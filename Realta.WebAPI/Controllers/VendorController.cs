using Microsoft.AspNetCore.Mvc;
//using Realta.Contract.Model;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public VendorController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<VendorController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var vendor = _repositoryManager.VendorRepository.FindAllVendor().ToList();
                return Ok(vendor);
            }
            catch (Exception)
            {
                _logger.LogError($"Error : {nameof(Get)}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // GET api/<VendorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VendorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VendorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
