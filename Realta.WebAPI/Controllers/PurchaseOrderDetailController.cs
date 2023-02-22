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
    public class PurchaseOrderDetailController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;

        public PurchaseOrderDetailController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // GET: api/<PurchaseOrderDetailController>/PO-20230222-001
        [HttpGet("{po}")]
        public async Task<IActionResult> Get(string po)
        {
            var result = await _repositoryManager.PurchaseOrderDetailRepository.FindAllAsync(po);
            var resultDto = result.Select(r => new PurchaseOrderDetailDto
            {
                PodeId = r.pode_id,
                PodePoheId = r.pode_pohe_id,
                PodeOrderQty = r.pode_order_qty,
                PodePrice = r.pode_price,
                PodeLineTotal = r.pode_line_total,
                PodeReceivedQty = r.pode_received_qty,
                PodeRejectedQty = r.pode_rejected_qty,
                PodeStockedQty = r.pode_stocked_qty,
                PodeModifiedDate = r.pode_modified_date,
                PodeStockId = r.pode_stock_id
            });

            return Ok(resultDto);
        }

        // GET api/<PurchaseOrderDetailController>/5
        [HttpGet("{id}", Name = "GetPodById")]
        public IActionResult GetById(int id)
        {
            var result = _repositoryManager.PurchaseOrderDetailRepository.FindById(id);
            if (result == null)
            {
                _logger.LogError($"PODetail with id {id} not found");
                return NotFound();
            }

            var resultDto = new PurchaseOrderDetailDto
            {
                PodeId = result.pode_id,
                PodePoheId = result.pode_pohe_id,
                PodeOrderQty = result.pode_order_qty,
                PodePrice = result.pode_price,
                PodeLineTotal = result.pode_line_total,
                PodeReceivedQty = result.pode_received_qty,
                PodeRejectedQty = result.pode_rejected_qty,
                PodeStockedQty = result.pode_stocked_qty,
                PodeModifiedDate = result.pode_modified_date,
                PodeStockId = result.pode_stock_id
            };

            return Ok(resultDto);
        }

        // POST api/<PurchaseOrderDetailController>
        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderDetailDto dto)
        {
            //1. prevent PODDTO from null
            if (dto == null)
            {
                _logger.LogError("PurchaseOrderDetail object sent from client is null");
                return BadRequest("PurchaseOrderDetail object is null");
            }


            var value = new PurchaseOrderDetail()
            {
                pode_pohe_id = dto.PodePoheId,
                pode_order_qty = dto.PodeOrderQty,
                pode_price = dto.PodePrice,
                pode_stock_id = dto.PodeStockId 
            };

            //post to db
            _repositoryManager.PurchaseOrderDetailRepository.Insert(value);

            var result = _repositoryManager.PurchaseOrderDetailRepository.FindById(value.pode_id);

            //forward 
            return CreatedAtRoute("GetPodById", new { id = value.pode_id }, result);
        }

        // PUT api/<PurchaseOrderDetailController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] QtyUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError($"POD with id {id} not found");
                return NotFound();
            }

            //1. prevent PODDTO from null
            if (dto == null)
            {
                _logger.LogError("Qty object sent from client is null");
                return BadRequest("Qty object is null");
            }

            var value = new PurchaseOrderDetail()
            {
                pode_id = id,
                pode_order_qty = dto.PodeOrderQty,
                pode_received_qty = dto.PodeReceivedQty,
                pode_rejected_qty = dto.PodeRejectedQty
            };

            _repositoryManager.PurchaseOrderDetailRepository.UpdateQty(value);
            var result = _repositoryManager.PurchaseOrderDetailRepository.FindById(id);

            //forward 
            return CreatedAtRoute("GetPodById", new { id }, result);
        }

        // DELETE api/<PurchaseOrderDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //1. prevent POHDTO from null
            if (id == null)
            {
                _logger.LogError("PurchaseOrderDetail object sent from client is null");
                return BadRequest("PurchaseOrderDetail object is null");
            }

            //2. find id first
            var result = _repositoryManager.PurchaseOrderDetailRepository.FindById(id.Value);
            if (result == null)
            {
                _logger.LogError($"POD with id {id} not found");
                return NotFound();
            }

            _repositoryManager.PurchaseOrderDetailRepository.Remove(result);
            return Ok("Data has been remove.");
        }
    }
}
