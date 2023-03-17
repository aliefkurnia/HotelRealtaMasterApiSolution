using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Realta.Contract.Models;
using Realta.Domain.Base;
using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;
using Realta.Services;
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
        private readonly  IServiceManager _serviceManager;
        public CategoryGroupController(IRepositoryManager repositoryManager, ILoggerManager logger, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _serviceManager = serviceManager;
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
                _logger.LogError("categoryGroupDto object sent from client is null");
                return BadRequest("categoryGroup object is null");
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


        //POST api/<Price_ItemsController>
        [HttpPost("UploadCagro"), DisableRequestSizeLimit]
        public async Task<IActionResult> CreateCategoryGroupPhoto()
        {
            //1. declare formCollection to hold form-data
            var formColletion = await Request.ReadFormAsync();

            //2. extract files to variable files
            var files = formColletion.Files;

            //3. hold each ouput formCollection to each variable
            formColletion.TryGetValue("CagroName", out var cagroName);
            formColletion.TryGetValue("CagroDescription", out var cagroDescription);
            formColletion.TryGetValue("CagroType", out var cagroType);
            formColletion.TryGetValue("CagroIcon", out var cagroIcon);
            formColletion.TryGetValue("CagroIconUrl", out var cagroIconUrl);

            //4. declare variable and store in object 
            var categoryGroupCreateDto = new CategoryGroupCreateDto
            {
                CagroName = cagroName.ToString(),
                CagroDescription = cagroDescription.ToString(),
                CagroType = cagroType.ToString(),
                CagroIcon = cagroIcon.ToString(),
                CagroIconUrl = cagroIconUrl.ToString(),
            };

            //5. store to list
            var allPhotos = new List<IFormFile>();
            foreach (var item in files)
            {
                allPhotos.Add(item);
            }

            //6. declare variable productphotogroup
            var categoryGroupPhotoGroup = new CategoryGroupPhotoGroupDto
            {
                CategoryGroupCreateDto = categoryGroupCreateDto,
                AllPhotos = allPhotos
            };

            if (categoryGroupPhotoGroup != null)
            {
                _serviceManager.CategoryGroupPhotoService.InsertCategoryGroupAndCategoryGroupPhoto(categoryGroupPhotoGroup, out var cagroId);
                var categroyGroupResult = _repositoryManager.CategoryGroupRepository.FindCategoryGroupById(cagroId);
                return Ok(categroyGroupResult);
            }
            _logger.LogError("CategoryGroupDto object sent from client is null");
            return BadRequest("Object Is Null");
        }


        [HttpGet("pageList")]
        public async Task<IActionResult> GetCategoryGroupPageList([FromQuery] CategoryGroupParameter categoryGroupParameter)
        {
            var cagro = await _repositoryManager.CategoryGroupRepository.GetCategoryGroupPageList(categoryGroupParameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(cagro.MetaData));
            return Ok(cagro);
        }
    }
}
