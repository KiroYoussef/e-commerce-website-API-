using Electronic.Api.Dtos;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService Service)
        {
            _service = Service;
        }
        [HttpGet]
        [Route("GetCategoryByID/{Id}")]
        public IActionResult GetCategoryById(int Id)
        {
            var cat = _service.GetCategoryById(Id);
            if (cat != null)
                return Ok(cat);
            return BadRequest("NO Category With This ID");
        }
        [HttpGet, Route("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            return Ok(_service.GetAllCategories());
        }

        [HttpPost, Route("AddNewCategory")]
        public IActionResult AddNewCategory(CategoryDTO catDTO)
        {
            if (ModelState.IsValid)
            {
                _service.AddNewCategory(catDTO);
                return Ok();
            }
            return BadRequest(catDTO);
        }
        [HttpPut]
        [Route("UpdateCategory/{Id}")]
        public IActionResult UpdateCategory([FromRoute] int Id, [FromBody] CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateCategory(Id, categoryDTO);
                return Ok();
            }
            return BadRequest(categoryDTO);
        }

        [HttpDelete("DeletCategory/{Id}")]
        public IActionResult DeletCategory(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = _service.DeletCategory(id);
            if (!model)
            {
                return BadRequest();
            }
            return Ok();
        }


    }
}

