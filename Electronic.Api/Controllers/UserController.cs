using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService Service)
        {
            _service = Service;
        }
        [HttpGet]
        [Route("GetUserImgByUerId/{Id}")]
        public IActionResult GetUserImgByUerId(string Id)
        {
            string img = "https://localhost:7096/img/Users/" + _service.GetUserPhotoByID(Id);

            return Ok(img);
        }

        [HttpGet]
        [Route("GetUser/{Id}")]
        public async Task<IActionResult> GetUser(string Id)
        {
            var user = await _service.GetUser(Id);

            return Ok(user);
        }



        [HttpGet]
        [Route("GetUserNameByUerId/{Id}")]
        public IActionResult GetUserNameByUerId(string Id)
        {
            string name = _service.GetUserNameByID(Id);
            return Ok(name);
        }
        [HttpGet]
        [Route("GetAllSeller")]
        public IActionResult GetAllSeller()
        {
            var SELLERS = _service.GetAllSeller();
            return Ok(SELLERS);
        }

    }
}
