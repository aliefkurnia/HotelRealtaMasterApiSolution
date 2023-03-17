using Microsoft.AspNetCore.Mvc;
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
                var Policy = _repositoryManager.PolicyRepository.FindAllPolicy().ToList();
                return Ok(Policy);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllPolicy)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<PolicyController>/5
        [HttpGet("{id}", Name = "GetPolicy")]
        public IActionResult FindPolicyById(int id)
        {
            var policy = _repositoryManager.PolicyRepository.FindPolicyById(id);
            if (policy == null)
            {
                _logger.LogError("Policy object sent from client is null");
                return BadRequest("Policy object not found");
            }
            var policyDto = new PolicyDto
            {
                PoliId = policy.PoliId,
                PoliName = policy.PoliName,
                PoliDescription = policy.PoliDescription,
            };
            return Ok(policyDto);
        }

        // GET api/<PolicyController>/
        [HttpGet("name/{name}", Name = "FindPolicyByName")]
        public IActionResult FindPolicyByName(string name)
        {
            var policy = _repositoryManager.PolicyRepository.FindPolicyByName(name);
            if (policy.ToList().Count() == 0)
            {
                _logger.LogError("PolicyDto object sent from client is null");
                return BadRequest("Policy object is null");
            }

            var policyDto = policy.Select(x => new PolicyDto
            {
                PoliId = x.PoliId,
                PoliName = x.PoliName,
                PoliDescription = x.PoliDescription,
            });
            return Ok(policy);
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
                    PoliId = policyDto.PoliId,
                    PoliName = policyDto.PoliName,
                    PoliDescription = policyDto.PoliDescription
                };

                //execute method Insert
                _repositoryManager.PolicyRepository.Insert(policy);

                policyDto.PoliId = policy.PoliId;
                return CreatedAtRoute("GetPolicy", new { id = policy.PoliId }, policyDto);
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
                    PoliId = id,
                    PoliName = policyDto.PoliName,
                    PoliDescription = policyDto.PoliDescription
                };
                _repositoryManager.PolicyRepository.Edit(policy);
                return CreatedAtRoute("GetPolicy", new { id = policyDto.PoliId }, new PolicyDto { PoliId = id, PoliName = policy.PoliName, PoliDescription = policy.PoliDescription });
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

                var policy = _repositoryManager.PolicyRepository.FindPolicyById(id);
                if (policy == null)
                {
                    _logger.LogError($"policy with {id} not found");
                    return NotFound();
                }
                _repositoryManager.PolicyRepository.Remove(policy);
                return Ok("Data has been removed");
            }

            [HttpGet("pageList")]
            public async Task<IActionResult> GetPriceItemsPageList([FromQuery] PolicyParameter policyParameter)
            {
                var priceitems = await _repositoryManager.PolicyRepository.GetPolicyPageList(policyParameter);
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(priceitems.MetaData));
                return Ok(priceitems);
            }
    }
    } 
