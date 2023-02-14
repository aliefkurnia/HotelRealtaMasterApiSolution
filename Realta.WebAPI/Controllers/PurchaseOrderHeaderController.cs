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
    public class PurchaseOrderHeaderController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;

        public PurchaseOrderHeaderController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }


        // GET: api/<PurchaseOrderHeaderController>
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

        // GET api/<PurchaseOrderHeaderController>/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var result = _repositoryManager.PurchaseOrderHeaderRepository.FindById(id);
            if (result == null)
            {
                _logger.LogError($"PO with id {id} not found");
                return NotFound();
            }

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
                PoheVendorId = result.pohe_vendor_id
            };



            return Ok(resultDto);
        }

        // POST api/<PurchaseOrderHeaderController>
        //[HttpPost]
        //public IActionResult Post([FromBody] PurchaseOrderHeaderDto[] headerDto, PurchaseOrderDetailDto[] detailDto)
        //{
        //    //1. prevent POHDTO from null
        //    if (headerDto == null || detailDto == null)
        //    {
        //        _logger.LogError("PurchaseOrder object sent from client is null");
        //        return BadRequest("PurchaseOrder object is null");
        //    }


        //    var value = new PurchaseOrderHeader()
        //    {
        //        //pohe_id = dto.PoheId,
        //        //pohe_order_date = dto.PoheOrderDate,
        //        //pohe_subtotal = dto.PoheSubtotal,
        //        //pohe_total_amount = dto.PoheTotalAmount,
        //        //pohe_status = dto.PoheStatus,

        //        pohe_number = dto.PoheNumber,
        //        pohe_tax = dto.PoheTax,
        //        pohe_refund = dto.PoheRefund,
        //        pohe_arrival_date = dto.PoheArrivalDate,
        //        pohe_pay_type = dto.PohePayType,
        //        pohe_emp_id = dto.PoheEmpId,
        //        pohe_vendor_id = dto.PoheVendorId
        //    };

        //    //post to db
        //    _repositoryManager.PurchaseOrderHeaderRepository.Insert(value);


        //    var result = _repositoryManager.PurchaseOrderHeaderRepository.FindById(value.pohe_id);

        //    //forward 
        //    return CreatedAtRoute("GetById", new { id = value.pohe_id }, result);
        //}

        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderHeaderDto[] headerDto, PurchaseOrderDetailDto[] detailDto)
        {
            // 1. Prevent POHDTO from null
            if (headerDto == null || detailDto == null)
            {
                _logger.LogError("PurchaseOrder object sent from client is null");
                return BadRequest("PurchaseOrder object is null");
            }

            // Loop through each header DTO and insert to database
            foreach (var header in headerDto)
            {
                var headerEntity = new PurchaseOrderHeader()
                {
                    pohe_number = header.PoheNumber,
                    pohe_tax = header.PoheTax,
                    pohe_refund = header.PoheRefund,
                    pohe_arrival_date = header.PoheArrivalDate,
                    pohe_pay_type = header.PohePayType,
                    pohe_emp_id = header.PoheEmpId,
                    pohe_vendor_id = header.PoheVendorId
                };

                _repositoryManager.PurchaseOrderHeaderRepository.Insert(headerEntity);

                // Loop through each detail DTO and insert to database
                foreach (var detail in detailDto)
                {
                    var detailEntity = new PurchaseOrderDetail()
                    {
                        pode_pohe_id = headerEntity.pohe_id,
                        pode_order_qty = detail.PodeOrderQty,
                        pode_price = detail.PodePrice,
                        pode_stock_id = detail.PodeStockId
                    };

                    //_repositoryManager.PurchaseOrderDetailRepository.Insert(detailEntity);
                }
            }

            // Save changes to database
            _repositoryManager.Save();

            return Ok();
        }


        // PUT api/<PurchaseOrderHeaderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StatusUpdateDto dto)
        {
            if (dto == null)
            {
                _logger.LogError($"PO with id {id} not found");
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
                pohe_id = id,
                pohe_status = dto.PoheStatus
            };

            _repositoryManager.PurchaseOrderHeaderRepository.UpdateStatus(value);
            var result = _repositoryManager.PurchaseOrderHeaderRepository.FindById(id);

            //forward 
            return CreatedAtRoute("GetById", new { id }, result);
        }

        // DELETE api/<PurchaseOrderHeaderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            //1. prevent POHDTO from null
            if (id == null)
            {
                _logger.LogError("PurchaseOrderHeader object sent from client is null");
                return BadRequest("PurchaseOrderHeader object is null");
            }

            //2. find id first
            var result = _repositoryManager.PurchaseOrderHeaderRepository.FindById(id.Value);
            if (result == null)
            {
                _logger.LogError($"PO with id {id} not found");
                return NotFound();
            }

            _repositoryManager.PurchaseOrderHeaderRepository.Remove(result);
            return Ok("Data has been remove.");
        }
    }
}
