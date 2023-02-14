using Microsoft.AspNetCore.Mvc;
using Realta.Contract;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StocksController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<StocksController>
        [HttpGet]
        public IActionResult Get()
        {
            var stocks = _repositoryManager.StockRepository.FindAllStocks().ToList();

            var stocksDto = stocks.Select(r => new StocksDto
            {
                StockId = r.stock_id,
                StockName = r.stock_name,
                StockDescription = r.stock_description,
                StockQuantity = r.stock_quantity,
                StockReorderPoint = r.stock_reorder_point,
                StockUsed = r.stock_used,
                StockScrap = r.stock_scrap,
                StockSize = r.stock_size,
                StockColor = r.stock_color,
                StockModifiedDate = r.stock_modified_date
            });

            return Ok(stocksDto);
        }

        // GET api/<StocksController>/5
        [HttpGet("{id}", Name = "GetStock")]
        public IActionResult FindAllStocksById(int id)
        {
            var stock = _repositoryManager.StockRepository.FindStocksById(id);
            if (stock == null)
            {
                _logger.LogError("Stocl object sent from client is null");
                return BadRequest("Stock object is null");
            }

            var stockDto = new StocksDto
            {
                StockId = stock.stock_id,
                StockName = stock.stock_name,
                StockDescription = stock.stock_description,
                StockQuantity= stock.stock_quantity,
                StockReorderPoint= stock.stock_reorder_point,
                StockUsed= stock.stock_used,
                StockScrap = stock.stock_scrap,
                StockSize = stock.stock_size,
                StockColor = stock.stock_color,
                StockModifiedDate = stock.stock_modified_date
            };

            return Ok(stockDto);
        }

        // POST api/<StocksController>
        [HttpPost]
        public IActionResult Post([FromBody] StocksDto stocksDto)
        {
            //1. prevent regionDto from is null
            if (stocksDto == null)
            {
                _logger.LogError("StockDto object sent from client is null");
                return BadRequest("StockDto object is null");
            }

            var stock = new Stocks 
            { 
                stock_name = stocksDto.StockName,
                stock_description = stocksDto.StockDescription,
                stock_quantity = stocksDto.StockQuantity,
                stock_reorder_point = stocksDto.StockReorderPoint,
                stock_used = stocksDto.StockUsed,
                stock_scrap = stocksDto.StockScrap,
                stock_size = stocksDto.StockSize,
                stock_color = stocksDto.StockColor,
                stock_modified_date = stocksDto.StockModifiedDate
            };

            // post 
            _repositoryManager.StockRepository.Insert(stock);
            stocksDto.StockId = stock.stock_id;

            //forward
            return CreatedAtRoute("GetStock", new { id = stock.stock_id }, stocksDto);
        }

        // PUT api/<StocksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StocksDto stocksDto)
        {
            if (stocksDto == null)
            {
                _logger.LogError("StockDto object sent from client is null");
                return BadRequest("StockDto object is null");
            }

            var stock = new Stocks {
                stock_id = id,
                stock_name = stocksDto.StockName,
                stock_description= stocksDto.StockDescription,
                stock_quantity= stocksDto.StockQuantity,
                stock_reorder_point= stocksDto.StockReorderPoint,
                stock_used= stocksDto.StockUsed,
                stock_scrap= stocksDto.StockScrap,  
                stock_size= stocksDto.StockSize,
                stock_color= stocksDto.StockColor,  
                stock_modified_date = DateTime.Now
            };

            _repositoryManager.StockRepository.Edit(stock);

            return CreatedAtRoute("GetStock", new { id = id }, new StocksDto 
                {
                    StockId = stock.stock_id,
                    StockName = stock.stock_name,
                    StockDescription = stock.stock_description,
                    StockQuantity = stock.stock_quantity,
                    StockReorderPoint = stock.stock_reorder_point,
                    StockUsed = stock.stock_used,
                    StockScrap = stock.stock_scrap,
                    StockSize = stock.stock_size,
                    StockColor = stock.stock_color,
                    StockModifiedDate = stock.stock_modified_date
                });
        }

        // DELETE api/<StocksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Id object sent from client is null");
                return BadRequest("Id object is null");
            }

            // find region by id
            var stock = _repositoryManager.StockRepository.FindStocksById(id.Value);
            if (stock == null)
            {
                _logger.LogError($"Stocks with id {id} not found");
                return NotFound();
            }

            _repositoryManager.StockRepository.Remove(stock);
            return Ok("Data has been remove");
        }

        // GET: api/<ProductController>
        [HttpGet, Route("findAllAsync")]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _repositoryManager.StockRepository.FindAllStocksAsync();
            return Ok(products.ToList());
        }

    }
}
