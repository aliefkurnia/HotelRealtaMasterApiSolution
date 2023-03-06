using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
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
                _logger.LogError("Object  sent from client is null");
                return BadRequest($"Object with id {id} is not found");
            }
            var vendorDto = new VendorDto
            {
                VendorEntityId = vendor.VendorEntityId,
                VendorName = vendor.VendorName,
                VendorActive = vendor.VendorActive,
                VendorPriority = vendor.VendorPriority,
                VendorRegisterTime = vendor.VendorRegisterTime,
                VendorWeburl = vendor.VendorWeburl,
                VendorModifiedDate = vendor.VendorModifiedDate
            };

            return Ok(vendorDto);

        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetVendorPaging([FromQuery] VendorParameters vendorParameters)
        {
            var products = await _repositoryManager.VendorRepository.GetVendorPaging(vendorParameters);
            return Ok(products);
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
                VendorEntityId = vendorDto.VendorEntityId,
                VendorName = vendorDto.VendorName,
                VendorActive = vendorDto.VendorActive,
                VendorPriority = vendorDto.VendorPriority,
                VendorRegisterTime = vendorDto.VendorRegisterTime,
                VendorWeburl = vendorDto.VendorWeburl,
                VendorModifiedDate = vendorDto.VendorModifiedDate
            };
            //post to database
            _repositoryManager.VendorRepository.Insert(vendor);
            //Redirect
            return CreatedAtRoute("GetVendor", new { id = vendorDto.VendorEntityId }, vendorDto);
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
                VendorEntityId = id,
                VendorName = vendorDto.VendorName,
                VendorActive = vendorDto.VendorActive,
                VendorPriority = vendorDto.VendorPriority,
                VendorWeburl = vendorDto.VendorWeburl
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
