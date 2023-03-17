﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Services.Abstraction;
using System.Reflection.PortableExecutable;
using System.Web;

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
        public async Task<IActionResult> Get([FromQuery] PurchaseOrderParameters param)
        {
            var result = await _repositoryManager.PurchaseOrderRepository.GetAllAsync(param);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));

            // return Ok(new 
            // { 
            //     status = "Success",
            //     message = "Success to fetch data.",
            //     data = result
            // });
            
            return Ok(result);
        }

        // GET api/<PurchaseOrderController>/PO-20211231-001
        [HttpGet("{poNumber}")]
        public async Task<IActionResult> GetByPo(string poNumber, [FromQuery] PurchaseOrderDetailParameters param)
        {
            // var result = _repositoryManager.PurchaseOrderRepository.FindAllDet(poNumber);
            var result = await _repositoryManager.PurchaseOrderRepository.GetAllDetAsync(poNumber, param);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result);
        }
        
        // GET api/<PurchaseOrderController>/PO-20211231-001
        [HttpGet("header/{poNumber}")]
        public IActionResult GetHeaderByPo(string poNumber)
        {
            // var result = _repositoryManager.PurchaseOrderRepository.FindAllDet(poNumber);
            var result = _repositoryManager.PurchaseOrderRepository.FindByPo(poNumber);
            return Ok(result);

        }

        // POST api/<PurchaseOrderController>
        [HttpPost]
        public IActionResult InsertPurchaseOrder([FromBody] PurchaseOrderTransfer[] data)
        {
            foreach (var i in data)
            {
                _repositoryManager.PurchaseOrderRepository.Insert(i);
            }

            return CreatedAtAction(nameof(InsertPurchaseOrder), new
            {
                status = "Success",
                message = "Purchase order has been created."
            });
        }

        // PUT api/<PurchaseOrderController>/detail/5
        [HttpPut("detail/{id}")]
        public IActionResult UpdateQty(int id, [FromBody] QtyUpdateDto dto)
        {
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
            return Ok(new
            {
                status = "Success",
                message = "Data has been updated."
            });
        }

        // PUT api/<PurchaseOrderController>/status/PO-20230222-001
        [HttpPut("status/{poNumber}")]
        public IActionResult UpdateStatus(string poNumber, [FromBody] StatusUpdateDto dto)
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
            return Ok(new
            {
                status = "Success",
                message = "Status has been updated."
            });

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
            return Ok(new
            {
                status = "Success",
                message = "Data has been removed."
            });
        }

        // DELETE api/<PurchaseOrderController>/detail/5
        [HttpDelete("detail/{id:int}")]
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
            return Ok(new
            {
                status = "Success",
                message = "Data has been removed."
            });
        }
    }
}
