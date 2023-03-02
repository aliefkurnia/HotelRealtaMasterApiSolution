using Microsoft.AspNetCore.Mvc;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Services.Abstraction;
using System.Reflection.PortableExecutable;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;

        public PurchaseOrderController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // GET: api/<PurchaseOrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repositoryManager.PurchaseOrderRepository.FindAllAsync();
            var resultDto = result.Select(r => new PurchaseOrderHeaderDto
            {
                PoheId = r.PoheId,
                PoheNumber = r.PoheNumber,
                PoheStatus = r.PoheStatus,
                PoheOrderDate = r.PoheOrderDate,
                PoheSubtotal = r.PoheSubtotal,
                PoheTax = r.PoheTax,
                PoheTotalAmount = r.PoheTotalAmount,
                PoheRefund = r.PoheRefund,
                PoheArrivalDate = r.PoheArrivalDate,
                PohePayType = r.PohePayType,
                PoheEmpId = r.PoheEmpId,
                PoheVendorId = r.PoheVendorId
            });

            return Ok(resultDto);
        }

        // GET api/<PurchaseOrderController>/PO-20211231-001
        [HttpGet("{poNumber}")]
        public async Task<IActionResult> Get(string poNumber)
        {
            var result = _repositoryManager.PurchaseOrderRepository.FindByPo(poNumber);
            if (result == null)
            {
                _logger.LogError($"POD with id {poNumber} not found");
                return NotFound();
            }

            var details = await _repositoryManager.PurchaseOrderRepository.FindAllDetAsync(poNumber);

            var resultDto = new PurchaseOrderHeaderDto
            {
                PoheId = result.PoheId,
                PoheNumber = result.PoheNumber,
                PoheStatus = result.PoheStatus,
                PoheOrderDate = result.PoheOrderDate,
                PoheSubtotal = result.PoheSubtotal,
                PoheTax = result.PoheTax,
                PoheTotalAmount = result.PoheTotalAmount,
                PoheRefund = result.PoheRefund,
                PoheArrivalDate = result.PoheArrivalDate,
                PohePayType = result.PohePayType,
                PoheEmpId = result.PoheEmpId,
                PoheVendorId = result.PoheVendorId,
                Details = details.Select(d => new PurchaseOrderDetailDto
                {
                    PodeId = d.PodeId,
                    PodePoheId = d.PodePoheId,
                    PodeOrderQty = d.PodeOrderQty,
                    PodePrice = d.PodePrice,
                    PodeLineTotal = d.PodeLineTotal,
                    PodeReceivedQty = d.PodeReceivedQty,
                    PodeRejectedQty = d.PodeRejectedQty,
                    PodeStockedQty = d.PodeStockedQty,
                    PodeModifiedDate = d.PodeModifiedDate,
                    PodeStockId = d.PodeStockId
                })
            };

            return Ok(resultDto);
        }

        // POST api/<PurchaseOrderController>
        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderDto dto)
        {
            var header = new PurchaseOrderHeader
            {
                PoheEmpId = dto.PoEmpId,
                PoheVendorId = dto.PoVendorId,
                PohePayType = dto.PoPayType,
            };
            var detail = new PurchaseOrderDetail
            {
                PodeOrderQty = dto.PoOrderQty,
                PodePrice = dto.PoPrice,
                PodeStockId = dto.PoStockId
            };

            _repositoryManager.PurchaseOrderRepository.Insert(header, detail);
            return Ok("Purchase order has been created");
        }

        // PUT api/<PurchaseOrderController>/5
        [HttpPut("detail/{id}")]
        public IActionResult PutDetail(int id, [FromBody] QtyUpdateDto dto)
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
                PodeId = id,
                PodeOrderQty = dto.PodeOrderQty,
                PodeReceivedQty = dto.PodeReceivedQty,
                PodeRejectedQty = dto.PodeRejectedQty
            };

            _repositoryManager.PurchaseOrderRepository.UpdateQty(value);

            //forward 
            return Ok("Data has been updated");
        }

        // DELETE api/<PurchaseOrderController>/detail/PO-20230222-001
        [HttpPut("status/{poNumber}")]
        public IActionResult PutStatus(string poNumber, [FromBody] StatusUpdateDto dto)
        {
            var data = _repositoryManager.PurchaseOrderRepository.FindByPo(poNumber);

            if (data == null)
            {
                _logger.LogError($"PO with po {poNumber} not found");
                return NotFound();
            }

            //1. prevent POHDTO from null
            if (dto == null)
            {
                _logger.LogError("Status object sent from client is null");
                return BadRequest("Status object is null");
            }

            var value = new PurchaseOrderHeader()
            {
                PoheNumber = poNumber,
                PoheStatus = dto.PoheStatus
            };

            _repositoryManager.PurchaseOrderRepository.UpdateStatus(value);

            //forward 
            return Ok("Status has been updated");

        }

        // DELETE api/<PurchaseOrderController>/PO-20230222-001
        [HttpDelete("{poNumber}")]
        public IActionResult DeleteHeader(string poNumber)
        {
            //1. prevent POHDTO from null
            if (poNumber == null)
            {
                _logger.LogError("PO Number sent from client is null");
                return BadRequest("PO Number is null");
            }

            //2. find id first
            var result = _repositoryManager.PurchaseOrderRepository.FindByPo(poNumber);
            if (result == null)
            {
                _logger.LogError($"PO with po {poNumber} not found");
                return NotFound();
            }

            _repositoryManager.PurchaseOrderRepository.Remove(result);
            return Ok("Data has been removed.");
        }

        // DELETE api/<PurchaseOrderController>/detail/5
        [HttpDelete("detail/{id}")]
        public IActionResult DeleteDetail(int? id)
        {
            //1. prevent POHDTO from null
            if (id == null)
            {
                _logger.LogError("Id sent from client is null");
                return BadRequest("Id is null");
            }

            //2. find id first
            var result = _repositoryManager.PurchaseOrderRepository.FindDetById(id.Value);
            if (result == null)
            {
                _logger.LogError($"POD with id {id} not found");
                return NotFound();
            }

            _repositoryManager.PurchaseOrderRepository.RemoveDetail(result);
            return Ok("Data has been removed.");
        }
    }
}
