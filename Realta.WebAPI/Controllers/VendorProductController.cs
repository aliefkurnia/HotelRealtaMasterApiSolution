using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
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
        public async Task<IActionResult> GetAsync()
        {
            var products = await _repositoryManager.VendorProductRepository.FindAllVendorProductAsync();
            return Ok(products.ToList());
        }

        // GET api/<VendorProductController>/5
        //[HttpGet("{id}", Name = "GetVenpro")]
        //public IActionResult GetVendorById(int id)
        //{
        //    try
        //    {
        //        var vendor = _repositoryManager.VendorProductRepository.GetVendorProduct(id);
        //        return Ok(vendor);
        //    }
        //    catch {

        //        return BadRequest("Object Not Found");
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenproPaging([FromQuery] VenproParameters venproParameters, int id)
        {
            //try
            //{
               var venpro = _repositoryManager.VendorProductRepository.GetVenpro(venproParameters, id);
               PagedList<VendorProduct> pagedList = await venpro;
               Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedList.MetaData));
               return Ok(pagedList);
          //  }
            //catch
            //{

            //    return BadRequest("Object Not Found");
            //}
        }
        //public async Task<IActionResult> FindById(int id)
        //{

        //    var vendpor = await _repositoryManager.VendorProductRepository.FindVendorProductByVendorId(id);
        //    if (vendpor == null)
        //    {
        //        _logger.LogError("Region object sent from client is null");
        //        return BadRequest($"Region with id {id} is not found");
        //    }
        //    var venporDto = vendpor.Select( v => new VendorProductDto
        //    {
        //        VeproId = v.VeproId,
        //        VendorName = v.VendorName,
        //        StockName = v.StockName,
        //        VeproQtyStocked = v.VeproQtyStocked,
        //        VeproQtyRemaining = v.VeproQtyRemaining,
        //        VeproPrice = v.VeproPrice,
        //        VenproStockId = v.VenproStockId,
        //        VeproVendorId = v.VeproVendorId
        //    });
        //    return Ok(venporDto);
        //}

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

           var validasi =  _repositoryManager.VendorProductRepository.ValidasiInsert(Dto.VenproStockId, Dto.VeproVendorId);
            //post to database
            if (validasi == true)
            {
                _repositoryManager.VendorProductRepository.Insert(venpro);
                // var result = _repositoryManager.VendorProductRepository.FindVendorProductById(venpro.VeproId);
                return Ok($"Object with Has been Inserted");//Redirect
            }
            else
            {
                return BadRequest("Data Input Has Been Exist");
            }
        }

        // PUT api/<VendorProductController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateVendorProduct(int id, [FromBody] VendorProductDto VenpoDto)
        {
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
            return Ok("Update Sucessfully");
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
