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
            var result = await _repositoryManager.PurchaseOrderHeaderRepository.FindAllAsync();
            var resultDto = result.Select(r => new PurchaseOrderHeaderDto
            {
                PoheId = r.pohe_id,
                PoheNumber = r.pohe_number,
                PoheStatus = r.pohe_status,
                PoheOrderDate = r.pohe_order_date,
                PoheSubtotal = r.pohe_subtotal,
                PoheTax = r.pohe_tax,
                PoheTotalAmount = r.pohe_total_amount,
                PoheRefund = r.pohe_refund,
                PoheArrivalDate = r.pohe_arrival_date,
                PohePayType = r.pohe_pay_type,
                PoheEmpId = r.pohe_emp_id,
                PoheVendorId = r.pohe_vendor_id
            });

            return Ok(resultDto);
        }

        // GET api/<PurchaseOrderController>/PO-20211231-001
        [HttpGet("{poNumber}")]
        public async Task<IActionResult> Get(string poNumber)
        {
            var result = _repositoryManager.PurchaseOrderHeaderRepository.FindByPo(poNumber);
            if (result == null)
            {
                _logger.LogError($"POD with id {poNumber} not found");
                return NotFound();
            }

            var details = await _repositoryManager.PurchaseOrderDetailRepository.FindAllAsync(poNumber);

            var resultDto = new PurchaseOrderHeaderDto
            {
                PoheId = result.pohe_id,
                PoheNumber = result.pohe_number,
                PoheStatus = result.pohe_status,
                PoheOrderDate = result.pohe_order_date,
                PoheSubtotal = result.pohe_subtotal,
                PoheTax = result.pohe_tax,
                PoheTotalAmount = result.pohe_total_amount,
                PoheRefund = result.pohe_refund,
                PoheArrivalDate = result.pohe_arrival_date,
                PohePayType = result.pohe_pay_type,
                PoheEmpId = result.pohe_emp_id,
                PoheVendorId = result.pohe_vendor_id,
                Details = details.Select(d => new PurchaseOrderDetailDto
                {
                    PodeId = d.pode_id,
                    PodePoheId = d.pode_pohe_id,
                    PodeOrderQty = d.pode_order_qty,
                    PodePrice = d.pode_price,
                    PodeLineTotal = d.pode_line_total,
                    PodeReceivedQty = d.pode_received_qty,
                    PodeRejectedQty = d.pode_rejected_qty,
                    PodeStockedQty = d.pode_stocked_qty,
                    PodeModifiedDate = d.pode_modified_date,
                    PodeStockId = d.pode_stock_id
                })
            };

            return Ok(resultDto);
        }

        // POST api/<PurchaseOrderController>
        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderDto[] dto)
        {
            if (dto == null)
            {
                _logger.LogError("PurchaseOrder object sent from client is null");
                return BadRequest("PurchaseOrder object is null");
            }

            dto = dto.OrderBy(p => p.PoVendorId).ToArray();

            int poId = -1;
            int previousVendorId = -1; // nilai awal sebelum ada data

            string currentDate = DateTime.Now.ToString("yyyyMMdd"); // ambil tanggal saat ini
            int sequenceNumber = 1; // nomor urut awal

            var lastPo = _repositoryManager.PurchaseOrderHeaderRepository.GetLastPoByDate(DateTime.Now);

            if (lastPo != null)
            {
                var lastPoNumber = lastPo.pohe_number;
                var lastSequenceNumber = Convert.ToInt32(lastPoNumber.Split("-")[2]); // ambil nomor urut dari nomor PO terakhir
                if (lastSequenceNumber >= 1 && lastSequenceNumber <= 998)
                {
                    sequenceNumber = lastSequenceNumber + 1; // gunakan nomor urut yang terakhir ditambah 1
                }
            }

            foreach (var data in dto)
            {
                string poNumber = $"PO-{currentDate}-{sequenceNumber:D3}"; // nomor PO pertama
                var existingPo = _repositoryManager.PurchaseOrderHeaderRepository.FindByPo(poNumber);

                if (existingPo != null)
                {
                    sequenceNumber++;
                    poNumber = $"PO-{currentDate}-{sequenceNumber:D3}";
                }
                //return Ok(existingPo);

                if (data.PoVendorId != previousVendorId)
                {
                    // vendor_id berbeda dari data sebelumnya
                    // lakukan sesuatu di sini
                    previousVendorId = data.PoVendorId;

                    var header = new PurchaseOrderHeader()
                    {
                        pohe_number = poNumber,
                        pohe_tax = 0.1M,
                        pohe_refund = 0,
                        pohe_pay_type = data.PoPayType,
                        pohe_emp_id = data.PoEmpId,
                        pohe_vendor_id = data.PoVendorId
                    };
                    _repositoryManager.PurchaseOrderHeaderRepository.Insert(header);
                    poId = header.pohe_id;
                    sequenceNumber++;
                }

                var detail = new PurchaseOrderDetail()
                {
                    pode_pohe_id = poId,
                    pode_order_qty = data.PoOrderQty,
                    pode_price = data.PoPrice,
                    pode_stock_id = data.PoStockId
                };

                //post to db
                _repositoryManager.PurchaseOrderDetailRepository.Insert(detail);
            }
            return Ok("Data[s] has been added.");
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
                pode_id = id,
                pode_order_qty = dto.PodeOrderQty,
                pode_received_qty = dto.PodeReceivedQty,
                pode_rejected_qty = dto.PodeRejectedQty
            };

            _repositoryManager.PurchaseOrderDetailRepository.UpdateQty(value);

            //forward 
            return Ok("Data has been updated");
        }

        // DELETE api/<PurchaseOrderController>/detail/PO-20230222-001
        [HttpPut("status/{poNumber}")]
        public IActionResult PutStatus(string poNumber, [FromBody] StatusUpdateDto dto)
        {
            var data = _repositoryManager.PurchaseOrderHeaderRepository.FindByPo(poNumber);

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
                pohe_number = poNumber,
                pohe_status = dto.PoheStatus
            };

            _repositoryManager.PurchaseOrderHeaderRepository.UpdateStatus(value);

            //forward 
            return Ok("Status has been updated");

        }

        // DELETE api/<PurchaseOrderController>/header/PO-20230222-001
        [HttpDelete("header/{poNumber}")]
        public IActionResult DeleteHeader(string poNumber)
        {
            //1. prevent POHDTO from null
            if (poNumber == null)
            {
                _logger.LogError("PO Number sent from client is null");
                return BadRequest("PO Number is null");
            }

            //2. find id first
            var result = _repositoryManager.PurchaseOrderHeaderRepository.FindByPo(poNumber);
            if (result == null)
            {
                _logger.LogError($"PO with po {poNumber} not found");
                return NotFound();
            }

            _repositoryManager.PurchaseOrderHeaderRepository.Remove(result);
            return Ok("Data has been remove.");
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
