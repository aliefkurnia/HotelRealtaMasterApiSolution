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
    public class Category_GroupController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public Category_GroupController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<Category_GroupController>
        [HttpGet]
        public IActionResult FindAllCategory_Group()
        {
            try
            {
                var country = _repositoryManager.category_groupRepository.FindAllCategory_Group().ToList();
                return Ok(country);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllCategory_Group)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Category_GroupController>/5
        [HttpGet("{id}", Name ="GetCategory_Group")]
        public IActionResult FindCategory_GroupById(int id)
        {
            var category_group = _repositoryManager.category_groupRepository.FindCategory_GroupById(id);
            if (category_group == null)
            {
                _logger.LogError("Country object sent from client is null");
                return BadRequest("Country object not found");
            }
            var category_groupDto = new Category_GroupDto
            {
                cagro_id = category_group.cagro_id,
                cagro_name = category_group.cagro_name,
                cagro_description = category_group.cagro_description,
                cagro_type = category_group.cagro_type,
                cagro_icon = category_group.cagro_icon,
                cagro_icon_url = category_group.cagro_icon_url,
            };
            return Ok(category_groupDto);
        }

        // POST api/<Category_GroupController>
        [HttpPost]
        public IActionResult CreateCategory_Group([FromBody] Category_GroupDto category_GroupDto)
        {
            if (category_GroupDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Region object is null");
            }
            var category_group = new Category_Group()
            {
                cagro_id = category_GroupDto.cagro_id,
                cagro_name = category_GroupDto.cagro_name,
                cagro_description = category_GroupDto.cagro_description,
                cagro_type = category_GroupDto.cagro_type,
                cagro_icon = category_GroupDto.cagro_icon,
                cagro_icon_url = category_GroupDto.cagro_icon_url,
            };

            _repositoryManager.category_groupRepository.Insert(category_group);
            category_GroupDto.cagro_id = category_group.cagro_id;
            return CreatedAtRoute("GetCategory_Group", new { id = category_GroupDto.cagro_id }, category_GroupDto);
        }

        // PUT api/<Category_GroupController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory_Group(int id, Category_GroupDto category_GroupDto)
        {
            if (category_GroupDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Country object is null");
            }

            var category_group = new Category_Group()
            {
                cagro_id = id,
                cagro_name = category_GroupDto.cagro_name,
                cagro_description = category_GroupDto.cagro_description,
                cagro_type = category_GroupDto.cagro_type,
                cagro_icon = category_GroupDto.cagro_icon,
                cagro_icon_url = category_GroupDto.cagro_icon_url,
            };
            _repositoryManager.category_groupRepository.Edit(category_group);
            return CreatedAtRoute("GetCategory_Group", new { id = category_GroupDto.cagro_id }, new Category_GroupDto { cagro_id = id, cagro_name = category_GroupDto.cagro_name, cagro_description= category_group.cagro_description,cagro_type= category_group.cagro_type,cagro_icon=category_group.cagro_icon, cagro_icon_url=category_group.cagro_icon_url });
        }

        // DELETE api/<Category_GroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("Catergory_GroupDto object sent from client is null");
                return BadRequest("Catergory_Group object is null");
            }

            var category_group = _repositoryManager.category_groupRepository.FindCategory_GroupById(id);
            if (category_group == null)
            {
                _logger.LogError($"Catergory_Group with {id} not found");
                return NotFound();
            }
            _repositoryManager.category_groupRepository.Remove(category_group);
            return Ok("Data has been removed");
        }
    }
}
