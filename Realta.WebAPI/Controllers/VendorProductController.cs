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
    public class VendorProductController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public VendorProductController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<VendorProductController>
        [HttpGet]
        //tanpa asyncronous
        //public IActionResult FetchAll()
        //{
        //    try
        //    {
        //        var venpor = _repositoryManager.VendorProductRepository.FindAllVendorProduct().ToList();
        //        return Ok(venpor);
        //    }
        //    catch (Exception)
        //    {
        //        _logger.LogError($"Error : {nameof(Get)}");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}

        public async Task<IActionResult> GetAsync()
        {
            var products = await _repositoryManager.VendorProductRepository.FindAllVendorProductAsync();    
            return Ok(products.ToList());
        }


        // GET api/<VendorProductController>/5
        [HttpGet("{id}", Name = "GetVenpro")]
        public IActionResult FindById(int id)
        {
            var vendpor = _repositoryManager.VendorProductRepository.FindVendorProductById(id);

            if (vendpor == null)
            {
                _logger.LogError("Region object sent from client is null");
                return BadRequest($"Region with id {id} is not found");
            }
            var venporDto = new VendorProduct
            {
                VeproId = vendpor.VeproId,
                VeproQtyStocked = vendpor.VeproQtyStocked,
                VeproQtyRemaining = vendpor.VeproQtyRemaining,
                VeproPrice = vendpor.VeproPrice,
                VenproStockId = vendpor.VenproStockId,
                VeproVendorId = vendpor.VeproVendorId
            };

            return Ok(venporDto);
        }

        // POST api/<VendorProductController>
        [HttpPost]
        public IActionResult CreateVendorProduct([FromBody] VendorProductDto Dto)
        {
            if (Dto == null)
            {
                _logger.LogError("Object sent from client is null");
                return BadRequest("Object is null");
            }

            var venpro = new VendorProduct()
            {
                VeproQtyStocked = Dto.VeproQtyStocked,
                VeproQtyRemaining= Dto.VeproQtyRemaining,
                VeproPrice = Dto.VeproPrice,
                VenproStockId = Dto.VenproStockId,
                VeproVendorId = Dto.VeproVendorId
            };

            //post to database
            _repositoryManager.VendorProductRepository.Insert(venpro);

            var result = _repositoryManager.VendorProductRepository.FindVendorProductById(venpro.VeproId);
            //Redirect
            return CreatedAtRoute("GetVenpro", new { id = venpro.VeproId }, result);
        }

        // PUT api/<VendorProductController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateVendorProduct(int id, [FromBody] VendorProductDto VenpoDto)
        {

            // lakukan validasi pada regiondto not null


            if (VenpoDto == null)
            {
                _logger.LogError("Object sent from client is null");
                return BadRequest("Object is null");
            }

            var venPo = new VendorProduct()
            {
                VeproId = id,
                VeproQtyStocked = VenpoDto.VeproQtyStocked,
                VeproQtyRemaining = VenpoDto.VeproQtyRemaining,
                VeproPrice = VenpoDto.VeproPrice
            };

            //post to database
            _repositoryManager.VendorProductRepository.Edit(venPo);
            var result = _repositoryManager.VendorProductRepository.FindVendorProductById(id);

            //Redirect
            return CreatedAtRoute("GetVenpro", new { id }, result);
        }

        // DELETE api/<VendorProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            var vendpro = _repositoryManager.VendorProductRepository.FindVendorProductById(id.Value);
           
            if (vendpro == null)
            {
                _logger.LogError($"Region with id \"{id}\" is not found");
                return BadRequest($"Object with id \"{id}\" is not found");
            }

            _repositoryManager.VendorProductRepository.Remove(vendpro);
            return Ok("Data Has Been Removed");
        }
    }
}
