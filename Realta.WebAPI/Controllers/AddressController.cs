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
    public class AddressController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public AddressController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult FindAllAddress()
        {
            try
            {
                var address = _repositoryManager.AddressRepository.FindAllAddress().ToList();
                return Ok(address);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllAddress)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}",Name ="GetAddress")]
        public IActionResult FindAddressById(int id)
        {
            var address = _repositoryManager.AddressRepository.FindAddressById(id);
            if (address == null)
            {
                _logger.LogError("Address object sent from client is null");
                return BadRequest("Address object not found");
            }
            var addressDto = new AddressDto
            {
                addr_id = address.addr_id,
                addr_line1 = address.addr_line1,
                addr_line2 = address.addr_line2,
                addr_postal_code = address.addr_postal_code,
                addr_spatial_location= address.addr_spatial_location,
                addr_prov_id=address.addr_prov_id,
            };
            return Ok(addressDto);
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                _logger.LogError("AddressDto object sent from client is null");
                return BadRequest("Address object is null");
            }

            var address = new Address()
            {
                addr_id = addressDto.addr_id,
                addr_line1 = addressDto.addr_line1,
                addr_line2 = addressDto.addr_line2,
                addr_postal_code = addressDto.addr_postal_code,
                addr_spatial_location = addressDto.addr_spatial_location,
                addr_prov_id = addressDto.addr_prov_id,
            };

            //execute method Insert
            _repositoryManager.AddressRepository.Insert(address);

            addressDto.addr_id = address.addr_id;
            return CreatedAtRoute("GetRegion", new { id = address.addr_id }, addressDto);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, AddressDto addressDto)
        {
            if (addressDto == null)
            {
                _logger.LogError("addressDto object sent from client is null");
                return BadRequest("address object is null");
            }

            var address = new Address
            {
                addr_id = id,
                addr_line1 = addressDto.addr_line1,
                addr_line2 = addressDto.addr_line2,
                addr_postal_code = addressDto.addr_postal_code,
                addr_spatial_location = addressDto.addr_spatial_location,
                addr_prov_id = addressDto.addr_prov_id,
            };

            _repositoryManager.AddressRepository.Edit(address);
            return CreatedAtRoute("GetAddress", new { id = addressDto.addr_id}, new AddressDto { addr_id = id, addr_line1 = address.addr_line1,addr_line2=address.addr_line2,addr_postal_code=address.addr_postal_code,addr_spatial_location=address.addr_spatial_location,addr_prov_id=address.addr_prov_id});
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("addressDto object sent from client is null");
                return BadRequest("Address object is null");
            }

            var address = _repositoryManager.AddressRepository.FindAddressById(id);
            if (address == null)
            {
                _logger.LogError($"address with {id} not found");
                return NotFound();
            }
            _repositoryManager.AddressRepository.Remove(address);
            return Ok("Data has been removed");
        }
    }
}
