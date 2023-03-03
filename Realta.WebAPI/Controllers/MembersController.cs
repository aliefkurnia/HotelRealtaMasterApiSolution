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
    public class MembersController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public MembersController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public IActionResult FindAllMembers()
        {
            try
            {
                var members = _repositoryManager.MembersRepository.FindAllMembers().ToList();
                return Ok(members);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllMembers)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}",Name ="GetMembers")]
        public IActionResult FindMembersById(string id)
        {
            var members = _repositoryManager.MembersRepository.FindMembersById(id);
            if (members == null)
            {
                _logger.LogError("members object sent from client is null");
                return BadRequest("members object not found");
            }
            var membersDto = new MembersDto
            {
                MembName = members.MembName,
                MembDescription = members.MembDescription
            };
            return Ok(membersDto);
        }

        // POST api/<MembersController>
        [HttpPost]
        public IActionResult CreateMembers([FromBody] MembersDto membersDto )
        {
            if (membersDto == null)
            {
                _logger.LogError("MembersDto object sent from client is null");
                return BadRequest("Members object is null");
            }
            var members = new Members()
            {
                MembName = membersDto.MembName,
                MembDescription = membersDto.MembDescription
            };

            _repositoryManager.MembersRepository.Insert(members);
            //var result = _repositoryManager.MembersRepository.FindMembersById(members.memb_name) ;
            return CreatedAtRoute("GetMembers", new { id = members.MembName }, members);
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(string id, MembersDto membersDto)
        {
            if (membersDto == null)
            {
                _logger.LogError("MembersDto object sent from client is null");
                return BadRequest("Members object is null");
            }

            var members = new Members()
            {
                MembName = membersDto.MembName,
                MembDescription = membersDto.MembDescription
            };
            _repositoryManager.MembersRepository.Edit(members);
            return CreatedAtRoute("GetMembers", new { id = membersDto.MembName}, new MembersDto { MembName= id, MembDescription= members.MembDescription});
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                _logger.LogError("MembersDto object sent from client is null");
                return BadRequest("Members object is null");
            }

            var members = _repositoryManager.MembersRepository.FindMembersById(id);
            if (members == null)
            {
                _logger.LogError($"Members with {id} not found");
                return NotFound();
            }
            _repositoryManager.MembersRepository.Remove(members);
            return Ok("Data has been removed");
        }
    }
}
