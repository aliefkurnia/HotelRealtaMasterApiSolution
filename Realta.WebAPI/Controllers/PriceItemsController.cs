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
    public class PriceItemsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public PriceItemsController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<Price_ItemsController>
        [HttpGet]
        public IActionResult FindAllPriceItems()
        {
            try
            {
                var priceItems = _repositoryManager.PriceItemsRepository.FindAllPriceItems().ToList();
                return Ok(priceItems);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllPriceItems)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Price_ItemsController>/5
        [HttpGet("id/{id}", Name = "GetPriceItems")]
        public IActionResult FindPriceItemsById(int id)
        {
            var priceItems = _repositoryManager.PriceItemsRepository.FindPriceItemsById(id);
            if (priceItems == null)
            {
                _logger.LogError("PriceItems object sent from client is null");
                return BadRequest("PriceItems object not found");
            }
            var priceItemsDto = new PriceItemsDto
            {
                PritId = priceItems.PritId,
                PritName = priceItems.PritName,
                PritPrice = priceItems.PritPrice,
                PritDescription = priceItems.PritDescription,
                PritType = priceItems.PritType,
            };
            return Ok(priceItemsDto);
        }

        // GET api/<Price_ItemsController>/
        [HttpGet("name/{name}", Name = "FindPriceItemsByName")]
        public IActionResult FindPriceItemsByName(string name)
        {
           var priceItems = _repositoryManager.PriceItemsRepository.FindPriceItemsByName(name);
            if (priceItems.ToList().Count() == 0)
            {
                _logger.LogError("PriceItemsDto object sent from client is null");
                return BadRequest("PriceItems object is null");
            }

            var priceItemsDto = priceItems.Select(x => new PriceItemsDto
            {
                PritId = x.PritId,
                PritName = x.PritName,
                PritPrice = x.PritPrice,
                PritDescription = x.PritDescription,
                PritType = x.PritType,
            });
            return Ok(priceItemsDto);
        }

        // POST api/<Price_ItemsController>
        [HttpPost]
        public IActionResult CreatePrice_Items([FromBody] PriceItemsDto priceItemsDto)
        {
            if (priceItemsDto == null)
            {
                _logger.LogError("PriceItemsDto object sent from client is null");
                return BadRequest("PriceItems object is null");
            }

            var priceItems = new PriceItems()
            {
                PritId = priceItemsDto.PritId,
                PritName = priceItemsDto.PritName,
                PritPrice = priceItemsDto.PritPrice,
                PritDescription = priceItemsDto.PritDescription,
                PritType = priceItemsDto.PritType,
                PritIconUrl= priceItemsDto.PritIconUrl,
            };

            //execute method Insert
            _repositoryManager.PriceItemsRepository.Insert(priceItems);

            priceItemsDto.PritId = priceItems.PritId;
            return CreatedAtRoute("GetPriceItems", new { id = priceItems.PritId}, priceItemsDto);
        }

        // PUT api/<Price_ItemsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePrice_Items(int id, [FromBody] PriceItemsDto priceItemsDto)
        {
            if (priceItemsDto == null)
            {
                _logger.LogError("PriceItemsDto object sent from client is null");
                return BadRequest("PriceItems object is null");
            }

            var priceItems = new PriceItems
            {
                PritId = id,
                PritName = priceItemsDto.PritName,
                PritPrice = priceItemsDto.PritPrice,
                PritDescription = priceItemsDto.PritDescription,
                PritType = priceItemsDto.PritType,
                PritIconUrl= priceItemsDto.PritIconUrl,
            };

            _repositoryManager.PriceItemsRepository.Edit(priceItems);
            return CreatedAtRoute("GetPriceItems", new { id = priceItemsDto.PritId }, new PriceItemsDto { PritId = id, PritName = priceItems.PritName, PritPrice = priceItems.PritPrice, PritDescription = priceItems.PritDescription, PritType = priceItems.PritType, PritIconUrl = priceItems.PritIconUrl });
        }

        // DELETE api/<Price_ItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("PriceItemsDto object sent from client is null");
                return BadRequest("PriceItems object is null");
            }

            var priceItems = _repositoryManager.PriceItemsRepository.FindPriceItemsById(id);
            if (priceItems == null)
            {
                _logger.LogError($"address with {id} not found");
                return NotFound();
            }
            _repositoryManager.PriceItemsRepository.Remove(priceItems);
            return Ok("Data has been removed");
        }
    }
}
