using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Model;
using Realta.Contract.Model;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using System.Numerics;

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
        [HttpGet("{id}", Name ="GetVendor")]
        public IActionResult FindVendorById(int id)
        {
            var vendor = _repositoryManager.VendorRepository.FindVendorById(id);

            if (vendor == null)
            {
                _logger.LogError("Region object sent from client is null");
                return BadRequest($"Region with id {id} is not found");
            }
            var vendorDto = new Vendor
            {
                vendor_entity_id = vendor.vendor_entity_id,
                vendor_name = vendor.vendor_name,
                vendor_active = vendor.vendor_active,
                vendor_priority = vendor.vendor_priority,
                vendor_register_time = vendor.vendor_register_time,
                vendor_weburl = vendor.vendor_weburl,
                vendor_modified_date = vendor.vendor_modified_date
            };

            return Ok(vendorDto);

        }
            // POST api/<VendorController>
            [HttpPost]
        public IActionResult CreateVendor([FromBody] VendorDto vendorDto)
        {
            // lakukan validasi pada regiondto not null
            if (vendorDto == null)
            {
                _logger.LogError("Object sent from client is null");
                return BadRequest("Object is null");
            }

            var vendor = new Vendor()
            {
                vendor_entity_id = vendorDto.vendor_entity_id,
                vendor_name = vendorDto.vendor_name,
                vendor_active = vendorDto.vendor_active,
                vendor_priority = vendorDto.vendor_priority,
                vendor_register_time = vendorDto.vendor_register_time,
                vendor_weburl = vendorDto.vendor_weburl,
                vendor_modified_date = vendorDto.vendor_modified_date
            };
            //post to database
            _repositoryManager.VendorRepository.Insert(vendor);

            //Redirect
            return CreatedAtRoute("GetVendor", new { id = vendorDto.vendor_entity_id }, vendorDto);
        }

        // PUT api/<VendorController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateVendor(int id, [FromBody] VendorDto vendorDto)
        {
            // lakukan validasi pada regiondto not null
            var result = _repositoryManager.VendorRepository.FindVendorById(id);

            if (result == null)
            {
                _logger.LogError("Object sent from client is null");
                return BadRequest("Object is null");
            }
            if (vendorDto == null)
            {
                _logger.LogError("Object sent from client is null");
                return BadRequest("Object is null");
            }

            var vendor = new Vendor()
            {
                vendor_entity_id = id,
                vendor_name = vendorDto.vendor_name,
                vendor_active = vendorDto.vendor_active,
                vendor_priority = vendorDto.vendor_priority,
                vendor_weburl = vendorDto.vendor_weburl
            };

            //post to database
            _repositoryManager.VendorRepository.Edit(vendor);

            //Redirect
            return CreatedAtRoute("GetVendor", new { id }, result );
        }

        // DELETE api/<VendorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id sent from client is null");
                return BadRequest($"Object is null");
            }

            var vendor = _repositoryManager.VendorRepository.FindVendorById(id.Value);
            if (vendor == null)
            {
                _logger.LogError($"Object with id \"{id}\" is not found");
                return BadRequest($"Object is null");
            }

            _repositoryManager.VendorRepository.Remove(vendor);
            return Ok("Data Has Been Removed");

        }
    }
}
