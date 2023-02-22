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
    public class PolicyController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public PolicyController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<PolicyController>
        [HttpGet]
        public IActionResult FindAllPolicy()
        {
            try
            {
                var Policy = _repositoryManager.policyRepository.FindAllPolicy().ToList();
                return Ok(Policy);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllPolicy)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<PolicyController>/5
        [HttpGet("{id}",Name ="GetPolicy")]
        public IActionResult FindPolicyById(int id)
        {
            var policy = _repositoryManager.policyRepository.FindPolicyById(id);
            if (policy == null)
            {
                _logger.LogError("Policy object sent from client is null");
                return BadRequest("Policy object not found");
            }
            var policyDto = new PolicyDto
            {
                poli_id = policy.poli_id,   
                poli_name = policy.poli_name,
                poli_description = policy.poli_description,
            };
            return Ok(policyDto);
        }

        // POST api/<PolicyController>
        [HttpPost]
        public IActionResult CreatePolicy([FromBody] PolicyDto policyDto)
        {
            if (policyDto == null)
            {
                _logger.LogError("PolicyDto object sent from client is null");
                return BadRequest("Policy object is null");
            }

            var policy = new Policy()
            {
                poli_id = policyDto.poli_id,
                poli_name = policyDto.poli_name,
                poli_description = policyDto.poli_description
            };

            //execute method Insert
            _repositoryManager.policyRepository.Insert(policy);

            policyDto.poli_id = policy.poli_id;
            return CreatedAtRoute("GetPolicy", new { id = policy.poli_id }, policyDto);
        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        public IActionResult UpdatePolicy(int id, PolicyDto policyDto)
        {
            if (policyDto == null)
            {
                _logger.LogError("PolicyDto object sent from client is null");
                return BadRequest("Policy object is null");
            }

            var policy = new Policy()
            {
                poli_id = id,
                poli_name = policyDto.poli_name,
                poli_description = policyDto.poli_description
            };
            _repositoryManager.policyRepository.Edit(policy);
            return CreatedAtRoute("GetPolicy", new { id = policyDto.poli_id }, new PolicyDto { poli_id = id, poli_name = policy.poli_name, poli_description = policy.poli_description });
        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("policyDto object sent from client is null");
                return BadRequest("policy object is null");
            }

            var policy = _repositoryManager.policyRepository.FindPolicyById(id);
            if (policy == null)
            {
                _logger.LogError($"policy with {id} not found");
                return NotFound();
            }
            _repositoryManager.policyRepository.Remove(policy);
            return Ok("Data has been removed");
        }
    }
}
