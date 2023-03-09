 using Microsoft.AspNetCore.Mvc;
using Realta.Contract;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Services.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockDetailController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public StockDetailController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<StockDetailController>/3
        [HttpGet("{stockId}")]
        public async Task<IActionResult> Get(int stockId)
        {
            var stockDetail = await _repositoryManager.StockDetailRepository.FindAllStockDetailByStockId(stockId);

            var stocksDetailDto = stockDetail.Select(r => new StockDetailDto
            {
                StodId = r.StodId,
                StodStockId = r.StodStockId,
                StodBarcodeNumber = r.StodBarcodeNumber,
                StodStatus = r.StodStatus,
                StodNotes = r.StodNotes,
                StodFaciId = r.StodFaciId,
                StodPoNumber = _repositoryManager.PurchaseOrderRepository.FindById(r.StodPoheId.Value).PoheNumber
            });

            var respon = new
            {
                Status = "success",
                Data = stocksDetailDto.ToList()
            };

            return Ok(respon);
        }

        // GET api/<StockDetailController>/5
        [HttpGet("/detail/{id}", Name = "GetStockDetail")]
        public IActionResult FindStockDetailById(int id)
        {
            var stockDetail = _repositoryManager.StockDetailRepository.FindStockDetailById(id);
            if (stockDetail == null)
            {
                _logger.LogError("Stock Detail object sent from client is null");
                return BadRequest("Stock Detail object is null");
            }

            var stockDetailDto = new StockDetailDto
            {
                StodId =stockDetail.StodId,
                StodStockId=stockDetail.StodStockId,
                StodBarcodeNumber=stockDetail.StodBarcodeNumber,
                StodStatus=stockDetail.StodStatus,
                StodNotes=stockDetail.StodNotes,
                StodFaciId=stockDetail.StodFaciId,
                StodPoNumber=_repositoryManager.PurchaseOrderRepository.FindById(stockDetail.StodPoheId.Value).PoheNumber
            };

            return Ok(stockDetailDto);
        }

        // PUT api/<StockDetailController>/5
        [HttpPut("switchStatus/{id}")]
        public IActionResult EditStatus(int id, [FromBody] StockDetailDto stockDetailDto)
        {
            if (stockDetailDto == null)
            {
                _logger.LogError("StockPhotoDto object sent from client is null");
                return BadRequest("StockPotoDto object is null");
            }

            var stockDetail = new StockDetail
            {   StodId = id,
                StodStatus = stockDetailDto.StodStatus,
                StodNotes = stockDetailDto.StodNotes,
                StodFaciId = stockDetailDto.StodFaciId
            };

            _repositoryManager.StockDetailRepository.SwitchStatus(stockDetail);
            var stockDetailStatus = _repositoryManager.StockDetailRepository.FindStockDetailById(id);

            return CreatedAtRoute("GetStockDetail", new {id = stockDetailStatus.StodId}, new
            {
                Status = "Success",
                Message = "Status from Id : " + id + " has been update",
                Data = stockDetailStatus
            });
        }

        // PUT api/<StockDetailController>/5
        [HttpPut("generateBarcodePo/{id}")]
        public IActionResult GenerateBarcode(int id, [FromBody] PurchaseOrderDetail purchaseOrderDetailDto)
        {
            if (purchaseOrderDetailDto == null)
            {
                _logger.LogError("purchasingOrderDetail object sent from client is null");
                return BadRequest("purchasingOrderDetail object is null");
            }

            var purchaseOrderDetail  = new PurchaseOrderDetail
            {
                PodeId = id,
                PodeOrderQty = purchaseOrderDetailDto.PodeOrderQty,
                PodeReceivedQty = purchaseOrderDetailDto.PodeReceivedQty,
                PodeRejectedQty = purchaseOrderDetailDto.PodeRejectedQty
            };

            _repositoryManager.StockDetailRepository.GenerateBarcodePO(purchaseOrderDetail);
            

            return Ok(new 
            {
                Status = "Success",
                Message = "Generate Barcode Is Success",
            });
        }

    }
}
