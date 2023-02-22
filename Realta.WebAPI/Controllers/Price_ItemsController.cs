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
    public class Price_ItemsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public Price_ItemsController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<Price_ItemsController>
        [HttpGet]
        public IActionResult FindAllPrice_Items()
        {
            try
            {
                var price_items = _repositoryManager.price_itemsRepository.FindAllPrice_Items().ToList();
                return Ok(price_items);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllPrice_Items)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Price_ItemsController>/5
        [HttpGet("{id}", Name = "GetPrice_Items")]
        public IActionResult FindPrice_ItemsById(int id)
        {
            var price_items = _repositoryManager.price_itemsRepository.FindPrice_ItemsById(id);
            if (price_items == null)
            {
                _logger.LogError("price_items object sent from client is null");
                return BadRequest("price_items object not found");
            }
            var price_itemsDto = new Price_ItemsDto
            {
                prit_id = price_items.prit_id,
                prit_name = price_items.prit_name,
                prit_price = price_items.prit_price,
                prit_description = price_items.prit_description,
                prit_type = price_items.prit_type,
            };
            return Ok(price_itemsDto);
        }

        // POST api/<Price_ItemsController>
        [HttpPost]
        public IActionResult CreatePrice_Items([FromBody] Price_ItemsDto price_ItemsDto)
        {
            if (price_ItemsDto == null)
            {
                _logger.LogError("AddressDto object sent from client is null");
                return BadRequest("Address object is null");
            }

            var price_items = new Price_Items()
            {
                prit_id = price_ItemsDto.prit_id,
                prit_name = price_ItemsDto.prit_name,
                prit_price = price_ItemsDto.prit_price,
                prit_description = price_ItemsDto.prit_description,
                prit_type = price_ItemsDto.prit_type,
            };

            //execute method Insert
            _repositoryManager.price_itemsRepository.Insert(price_items);

            price_ItemsDto.prit_id = price_items.prit_id;
            return CreatedAtRoute("GetRegion", new { id = price_items.prit_id}, price_ItemsDto);
        }

        // PUT api/<Price_ItemsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePrice_Items(int id, [FromBody] Price_ItemsDto price_ItemsDto)
        {
            if (price_ItemsDto == null)
            {
                _logger.LogError("addressDto object sent from client is null");
                return BadRequest("address object is null");
            }

            var price_items = new Price_Items
            {
                prit_id = id,
                prit_name = price_ItemsDto.prit_name,
                prit_price = price_ItemsDto.prit_price,
                prit_description = price_ItemsDto.prit_description,
                prit_type = price_ItemsDto.prit_type
            };

            _repositoryManager.price_itemsRepository.Edit(price_items);
            return CreatedAtRoute("GetPrice_Items", new { id = price_ItemsDto.prit_id }, new Price_ItemsDto { prit_id = id, prit_name = price_items.prit_name, prit_price = price_items.prit_price, prit_description = price_items.prit_description, prit_type = price_items.prit_type });
        }

        // DELETE api/<Price_ItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("Price_itemsDto object sent from client is null");
                return BadRequest("Price_items object is null");
            }

            var price_Items = _repositoryManager.price_itemsRepository.FindPrice_ItemsById(id);
            if (price_Items == null)
            {
                _logger.LogError($"address with {id} not found");
                return NotFound();
            }
            _repositoryManager.price_itemsRepository.Remove(price_Items);
            return Ok("Data has been removed");
        }
    }
}
