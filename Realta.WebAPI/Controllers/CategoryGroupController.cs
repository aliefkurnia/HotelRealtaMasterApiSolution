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
    public class CategoryGroupController : ControllerBase
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public CategoryGroupController(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }


        // GET: api/<Category_GroupController>
        [HttpGet]
        public IActionResult FindAllCategoryGroup()
        {
            try
            {
                var categoryGroup = _repositoryManager.CategoryGroupRepository.FindAllCategoryGroup().ToList();
                return Ok(categoryGroup);
            }
            catch (Exception)
            {

                _logger.LogError($"Error : {nameof(FindAllCategoryGroup)}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET api/<Category_GroupController>/5
        [HttpGet("{id}", Name ="GetCategoryGroup")]
        public IActionResult FindCategoryGroupById(int id)
        {
            var categoryGroup = _repositoryManager.CategoryGroupRepository.FindCategoryGroupById(id);
            if (categoryGroup == null)
            {
                _logger.LogError("CategoryGroup object sent from client is null");
                return BadRequest("CategoryGroup object not found");
            }
            var categoryGroupDto = new CategoryGroupDto
            {
                CagroId = categoryGroup.CagroId,
                CagroName = categoryGroup.CagroName,
                CagroDescription = categoryGroup.CagroDescription,
                CagroType = categoryGroup.CagroType,
                CagroIcon = categoryGroup.CagroIcon,
                CagroIconUrl = categoryGroup.CagroIconUrl,
            };
            return Ok(categoryGroupDto);
        }

        // POST api/<Category_GroupController>
        [HttpPost]
        public IActionResult CreateCategoryGroup([FromBody] CategoryGroupDto categoryGroupDto)
        {
            if (categoryGroupDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Region object is null");
            }
            var categoryGroup = new CategoryGroup()
            {
                CagroId = categoryGroupDto.CagroId,
                CagroName = categoryGroupDto.CagroName,
                CagroDescription = categoryGroupDto.CagroDescription,
                CagroType = categoryGroupDto.CagroType,
                CagroIcon = categoryGroupDto.CagroIcon,
                CagroIconUrl = categoryGroupDto.CagroIconUrl,
            };

            _repositoryManager.CategoryGroupRepository.Insert(categoryGroup);
            categoryGroupDto.CagroId = categoryGroup.CagroId;
            return CreatedAtRoute("GetCategory_Group", new { id = categoryGroupDto.CagroId }, categoryGroupDto);
        }

        // PUT api/<Category_GroupController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategoryGroup(int id, CategoryGroupDto categoryGroupDto)
        {
            if (categoryGroupDto == null)
            {
                _logger.LogError("CountryDto object sent from client is null");
                return BadRequest("Country object is null");
            }

            var categoryGroup = new CategoryGroup()
            {
                CagroId = id,
                CagroName = categoryGroupDto.CagroName,
                CagroDescription = categoryGroupDto.CagroDescription,
                CagroType = categoryGroupDto.CagroType,
                CagroIcon = categoryGroupDto.CagroIcon,
                CagroIconUrl = categoryGroupDto.CagroIconUrl,
            };
            _repositoryManager.CategoryGroupRepository.Edit(categoryGroup);
            return CreatedAtRoute("GetCategoryGroup", new { id = categoryGroupDto.CagroId }, new CategoryGroupDto { CagroId = id, CagroName = categoryGroupDto.CagroName, CagroDescription= categoryGroup.CagroDescription,CagroType= categoryGroup.CagroType,CagroIcon=categoryGroup.CagroIcon, CagroIconUrl=categoryGroup.CagroIconUrl });
        }

        // DELETE api/<Category_GroupController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                _logger.LogError("CatergoryGroupDto object sent from client is null");
                return BadRequest("CatergoryGroup object is null");
            }

            var categoryGroup = _repositoryManager.CategoryGroupRepository.FindCategoryGroupById(id);
            if (categoryGroup == null)
            {
                _logger.LogError($"CatergoryGroup with {id} not found");
                return NotFound();
            }
            _repositoryManager.CategoryGroupRepository.Remove(categoryGroup);
            return Ok("Data has been removed");
        }
    }
}
