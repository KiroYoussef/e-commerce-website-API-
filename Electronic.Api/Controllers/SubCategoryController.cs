using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _service;
        public SubCategoryController(ISubCategoryService Service)
        {
            _service = Service;
        }
        [HttpGet]
        [Route("GetSubCategoryByID/{Id}")]
        public IActionResult GetSubCategoryById(int Id)
        {
            return Ok(_service.GetSubCategoryById(Id));
        }
        [HttpGet]
        [Route("GetAllSubCategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_service.GetAllSubCategories());
        }
        [HttpPost]
        [Route("AddNewSubCategory")]
        public IActionResult AddNewCategory(SubCatgoryDto subcatDTO)
        {
            if (ModelState.IsValid)
            {
                _service.AddNewSubCategory(subcatDTO);
                return Ok();
            }
            return BadRequest(subcatDTO);
        }
        [HttpPut]
        [Route("UpdateSubCategory/{Id}")]
        public IActionResult UpdateSubCategory(int Id, [FromBody] SubCatgoryDto SubcategoryDTO)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateSubCategory(Id, SubcategoryDTO);
                return Ok();
            }
            return BadRequest(SubcategoryDTO);
        }
        [HttpDelete("DeletSubCategory/{Id}")]
        public async Task<IActionResult> DeletSubCategory(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = await _service.DeletSubCategory(id);
            if (!model)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}