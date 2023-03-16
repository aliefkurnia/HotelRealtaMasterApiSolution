using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Domain.Entities;
using Realta.Domain.Base;
using Realta.Services.Abstraction;
using Realta.Contract.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Realta.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;
        public CartController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> GetCart(int? empId)
        {
            var result = await _repositoryManager.CartRepository.GetAllAsync(empId);
            return Ok(result);
        }

        // POST api/<CartController>
        [HttpPost]
        public IActionResult InsertCart([FromBody] CartDto cart)
        {
            var dto = new Cart
            {
                CartVeproId = cart.CartVeproId,
                CartEmpId = cart.CartEmpId,
                CartOrderQty = cart.CartOrderQty,
            };

            _repositoryManager.CartRepository.Insert(dto);
            return CreatedAtAction(nameof(InsertCart), new
            {
                status = "Success",
                message = "Cart has been inserted."
            });
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateQty(int id, [FromBody] CartDto cart)
        {
            var data = new Cart
            {
                CartId = id,
                CartOrderQty = cart.CartOrderQty
            };
            _repositoryManager.CartRepository.UpdateQty(data, out bool status);
            if (status)
            {
                return Ok(new
                {
                    status = "Success",
                    message = "Cart has been updated."
                });
            }
            else
            {
                _logger.LogError($"Cart with id {id} not found");
                return NotFound();
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repositoryManager.CartRepository.Remove(id, out bool status);
            if (status)
            {
                return Ok(new
                {
                    status = "Success",
                    message = "Cart has been deleted."
                });
            }
            else
            {
                _logger.LogError($"Cart with id {id} not found");
                return NotFound();
            }
        }
    }
}
