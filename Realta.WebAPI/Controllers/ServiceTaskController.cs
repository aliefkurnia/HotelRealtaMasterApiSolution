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
    public class ServiceTaskController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public ServiceTaskController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        // GET: api/<Service_TaskController>
        [HttpGet]
        public IActionResult FindAllServiceTask()
        {
            try
            {
                var servicetask = _repositoryManager.ServiceTaskRepository.FindAllServiceTask().ToList();
                return Ok(servicetask);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllServiceTask)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Service_TaskController>/5
        [HttpGet("{id}",Name ="GetServiceTask")]
        public IActionResult FindServiceTaskById(int id)
        {
            var serviceTask = _repositoryManager.ServiceTaskRepository.FindServiceTaskById(id);
            if (serviceTask == null)
            {
                _logger.LogError("serviceTask object sent from client is null");
                return BadRequest("serviceTask object not found");
            }
            var serviceTaskDto = new ServiceTaskDto
            {
                SetaId = serviceTask.SetaId,
                SetaName = serviceTask.SetaName,
                SetaSeq = serviceTask.SetaSeq
            };
            return Ok(serviceTaskDto);
        }

        // POST api/<Service_TaskController>
        [HttpPost]
        public IActionResult CreateServiceTask([FromBody] ServiceTaskDto serviceTaskDto)
        {
            if (serviceTaskDto == null)
            {
                _logger.LogError("service_TaskDto object sent from client is null");
                return BadRequest("service_Task object is null");
            }
            var serviceTask = new ServiceTask()
            {
                SetaId = serviceTaskDto.SetaId,
                SetaName = serviceTaskDto.SetaName,
                SetaSeq = serviceTaskDto.SetaSeq
            };

            _repositoryManager.ServiceTaskRepository.Insert(serviceTask);
            serviceTaskDto.SetaId = serviceTask.SetaId;
            return CreatedAtRoute("GetServiceTask", new { id = serviceTaskDto.SetaId }, serviceTaskDto);
        }

        // PUT api/<Service_TaskController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateServiceTask(int id, [FromBody] ServiceTask serviceTaskDto)
        {
            if (serviceTaskDto == null)
            {
                _logger.LogError("service_TaskDto object sent from client is null");
                return BadRequest("service_Task object is null");
            }

            var serviceTask = new ServiceTask()
            {
                SetaId = id,
                SetaName = serviceTaskDto.SetaName,
                SetaSeq = serviceTaskDto.SetaSeq
            };
            _repositoryManager.ServiceTaskRepository.Edit(serviceTask);
            return CreatedAtRoute("GetServiceTask", new { id = serviceTaskDto.SetaId }, new ServiceTaskDto { SetaId = id, SetaName = serviceTask.SetaName, SetaSeq = serviceTask.SetaSeq });
        }

        // DELETE api/<Service_TaskController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("serviceTaskDto object sent from client is null");
                return BadRequest("serviceTask object is null");
            }

            var servicetask = _repositoryManager.ServiceTaskRepository.FindServiceTaskById(id);
            if (servicetask == null)
            {
                _logger.LogError($"serviceTask with {id} not found");
                return NotFound();
            }
            _repositoryManager.ServiceTaskRepository.Remove(servicetask);
            return Ok("Data has been removed");
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetRegionsPageList([FromQuery] ServiceTaskParameter serviceTaskParameter)
        {
            var seta = await _repositoryManager.ServiceTaskRepository.GetServiceTaskPageList(serviceTaskParameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(seta.MetaData));
            return Ok(seta);
        }
    }
}
