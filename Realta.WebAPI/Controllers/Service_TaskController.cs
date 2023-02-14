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
    public class Service_TaskController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public Service_TaskController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<Service_TaskController>
        [HttpGet]
        public IActionResult FindAllService_Task()
        {
            try
            {
                var service_task = _repositoryManager.service_TaskRepository.FindAllService_Task().ToList();
                return Ok(service_task);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllService_Task)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Service_TaskController>/5
        [HttpGet("{id}",Name ="GetService_Task")]
        public IActionResult FindService_TaskById(int id)
        {
            var service_task = _repositoryManager.service_TaskRepository.FindService_TaskById(id);
            if (service_task == null)
            {
                _logger.LogError("service_task object sent from client is null");
                return BadRequest("service_task object not found");
            }
            var service_TaskDto = new Service_TaskDto
            {
                seta_id = service_task.seta_id,
                seta_name = service_task.seta_name,
                seta_seq = service_task.seta_seq
            };
            return Ok(service_TaskDto);
        }

        // POST api/<Service_TaskController>
        [HttpPost]
        public IActionResult CreateService_Task([FromBody] Service_TaskDto service_TaskDto)
        {
            if (service_TaskDto == null)
            {
                _logger.LogError("service_TaskDto object sent from client is null");
                return BadRequest("service_Task object is null");
            }
            var service_task = new Service_Task()
            {
                seta_id = service_TaskDto.seta_id,
                seta_name = service_TaskDto.seta_name,
                seta_seq = service_TaskDto.seta_seq
            };

            _repositoryManager.service_TaskRepository.Insert(service_task);
            service_TaskDto.seta_id = service_task.seta_id;
            return CreatedAtRoute("GetService_Task", new { id = service_TaskDto.seta_id }, service_TaskDto);
        }

        // PUT api/<Service_TaskController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateService_Task(int id, [FromBody] Service_Task service_TaskDto)
        {
            if (service_TaskDto == null)
            {
                _logger.LogError("service_TaskDto object sent from client is null");
                return BadRequest("service_Task object is null");
            }

            var service_task = new Service_Task()
            {
                seta_id = id,
                seta_name = service_TaskDto.seta_name,
                seta_seq = service_TaskDto.seta_seq
            };
            _repositoryManager.service_TaskRepository.Edit(service_task);
            return CreatedAtRoute("GetService_Task", new { id = service_TaskDto.seta_id }, new Service_TaskDto { seta_id = id, seta_name = service_task.seta_name, seta_seq = service_task.seta_seq });
        }

        // DELETE api/<Service_TaskController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("service_TaskDto object sent from client is null");
                return BadRequest("service_Task object is null");
            }

            var service_task = _repositoryManager.service_TaskRepository.FindService_TaskById(id);
            if (service_task == null)
            {
                _logger.LogError($"service_task with {id} not found");
                return NotFound();
            }
            _repositoryManager.service_TaskRepository.Remove(service_task);
            return Ok("Data has been removed");
        }
    }
}
