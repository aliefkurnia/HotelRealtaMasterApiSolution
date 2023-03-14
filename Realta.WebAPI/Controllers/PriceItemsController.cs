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
    public class PriceItemsController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IServiceManager _serviceManager;
        public PriceItemsController(IRepositoryManager repositoryManager, ILoggerManager logger, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _serviceManager = serviceManager;
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
                _logger.LogError($"Price Items with {id} not found");
                return NotFound();
            }
            _repositoryManager.PriceItemsRepository.Remove(priceItems);
            return Ok("Data has been removed");
        }

        //POST api/<Price_ItemsController>
        [HttpPost("UploadPrit"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreatePriceItemsPhoto()
        {
            //1. declare formCollection to hold form-data
            var formColletion = await Request.ReadFormAsync();

            //2. extract files to variable files
            var files = formColletion.Files;

            //3. hold each ouput formCollection to each variable
            formColletion.TryGetValue("PritName", out var pritName);
            formColletion.TryGetValue("PritPrice", out var pritPrice);
            formColletion.TryGetValue("PritDescription", out var pritDescription);
            formColletion.TryGetValue("PritType", out var pritType);

            //4. declare variable and store in object 
            var priceItemsCreateDto = new PriceItemsCreateDto
            {
                PritName = pritName.ToString(),
                PritPrice = decimal.Parse(pritPrice.ToString()),
                PritDescription = pritDescription.ToString(),
                PritType = pritType.ToString(),
                PritIconUrl = $"localhost:7068/resources/images/{pritName}",
                PritModifiedDate = DateTime.Now
            };

            //5. store to list
            var allPhotos = new List<IFormFile>();
            foreach (var item in files)
            {
                allPhotos.Add(item);
            }

            //6. declare variable productphotogroup
            var priceItemsPhotoGroup = new PriceItemsPhotoGroupDto
            {
                PriceItemsForCreateDto = priceItemsCreateDto,
                AllPhotos = allPhotos
            };

            if (priceItemsPhotoGroup != null)
            {
                _serviceManager.PriceItemsPhotoService.InsertPriceItemsAndPriceItemsPhoto(priceItemsPhotoGroup, out var pritId);
                var priceItemsResult = _repositoryManager.PriceItemsRepository.FindPriceItemsById(pritId);
                return Ok(priceItemsResult);
            }
            _logger.LogError("PriceItemsDto object sent from client is null");
            return BadRequest("Object Is Null");
        }


        [HttpGet("paging")]
        public async Task<IActionResult> GetPriceItemsPaging([FromQuery] PriceItemsParameters priceItemsParameters)
        {
            var priceitems = await _repositoryManager.PriceItemsRepository.GetPriceItemsPaging(priceItemsParameters);
            return Ok(priceitems);
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetPriceItemsPageList([FromQuery] PriceItemsParameters priceItemsParameters)
        {
            if (!priceItemsParameters.ValidateStockRange)
                return BadRequest("MaxStock must Greater than MinStock");
            
            var priceitems = await _repositoryManager.PriceItemsRepository.GetPriceItemsPageList(priceItemsParameters);
            Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(priceitems.MetaData));
            return Ok(priceitems);
        }

    }
}
