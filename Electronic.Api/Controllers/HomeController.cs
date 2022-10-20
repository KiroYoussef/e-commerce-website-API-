using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService service)
        {
            _service = service;
        }

        [HttpGet, Route("GetAllProductsNew")]
        public IActionResult GetAllProductsNew()
        {
            return Ok(_service.GetAllProductsNew());
        }




        [HttpGet, Route("GetAllPhones")]
        public IActionResult GetAllPhones(string? UserID)
        {
            return Ok(_service.GetAllPhones(UserID));
        }


        [HttpGet, Route("GetAllLabtop")]
        public IActionResult GetAllLabtop(string? UserID)
        {
            return Ok(_service.GetAllLabtops(UserID));
        }


        [HttpGet, Route("GetAllAccessories")]
        public IActionResult GetAllAccessories(string? UserID)
        {
            return Ok(_service.GetAllAccessories(UserID));
        }







        [HttpGet, Route("ByIdCateoryAllPoduc")]
        public IActionResult ByIdCateoryAllPoduct(int id, string? UserID)
        {
            return Ok(_service.ByIdCateoryAllPoduct(id, UserID));
        }


        [HttpGet, Route("GetAllSubCategoryByIdCaty/{id}")]
        public IActionResult GetAllSubCategoryByIdCaty(int id)
        {
            return Ok(_service.GetAllSubCategoryByIdCaty(id));
        }



        [HttpGet, Route("GetAllProductByIdSubCategory")]
        public IActionResult GetAllProductByIdSubCategory(int id, string? UserID)
        {
            return Ok(_service.GetAllProductByIdSubCategory(id, UserID));
        }



    }
}
