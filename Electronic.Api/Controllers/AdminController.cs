using AutoMapper;
using Electronic.Api.Model;
using Electronic.Api.Model.user;
using Electronic.Api.Repository;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBaseRepository<ApplicationUser> genericRepository;
        private readonly IAdminService adminService;
        private readonly IMapper mapper;
        private readonly IBaseRepository<IdentityRole> roleRepository;
        private readonly IProductService prodservice;

        public AdminController(IBaseRepository<ApplicationUser> genericRepository,
            IAdminService adminService, IMapper mapper,
            IBaseRepository<IdentityRole> roleRepository,
            IProductService prodservice)
        {
            this.genericRepository = genericRepository;
            this.adminService = adminService;
            this.mapper = mapper;
            this.roleRepository = roleRepository;
            this.prodservice = prodservice;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            return Ok(genericRepository.GetAll());

            //try
            //{
            //    return Ok(genericRepository.GetAll());
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);

            //}

        }
        [HttpGet("CountAdmin")]
        public ActionResult CountAdmin()
        {
            try
            {
                return Ok(adminService.CountAdmin());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpPost("Adduser")]
        public async Task<IActionResult> Adduser(AddUserModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await adminService.AddUserAsync(model);
                if (user != null)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            if (id == null) { return NotFound(); }

            var getid = genericRepository.GetByIds(id);
            if (getid != null) { return Ok(getid); }
            return BadRequest();

        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(EditUserModel model)
        {
            if (model == null) { return NotFound(); }


            var data = await adminService.EditUserAsync(model);

            if (data != null) { return Ok(data); }
            return BadRequest();

        }

        [HttpPost("DeleteUsers")]
        public async Task<IActionResult> DeleteUsers(List<string> model)
        {

            if (model == null)
            {
                return NotFound();
            }
            var result = await adminService.DeleteUsersAsync(model);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();



        }


        [HttpGet("GetUserRole")]
        public async Task<ActionResult<IEnumerable<UserRolesModel>?>> GetUserRole()
        {
            var userRoles = await adminService.GetUserRoleAsync();
            if (userRoles == null)
            {
                return NotFound();
            }
            return Ok(userRoles);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var user = roleRepository.GetAll();
            return Ok(user);
        }


        [HttpPut("EditUserRole")]
        public async Task<IActionResult> EditUserRole(EditUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var x = await adminService.EditUserRoleAsync(model);
                if (x)
                {
                    return Ok();
                }
            }
            return BadRequest();

        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            return Ok(adminService.GetAllProducts());
        }

        //[HttpGet("GellAllProductNeedReview")]
        //public IActionResult GellAllProductNeedReview()
        //{
        //    return Ok(prodservice.GellAllProductNeedReview());
        //}



        [HttpGet]
        [Route("UpdateProductApprove/{ProductId}/{firstApprove}")]
        public IActionResult UpdateProductApprove(int ProductId, int firstApprove)
        {
            if (prodservice.UpdateProductApprove(ProductId, firstApprove))
            {
                return Ok("product Approved");

            }
            return Ok("product Not Found");
        }


        [HttpDelete]
        [Route("RemoveProduct/{Id}")]
        public IActionResult Remove(int Id)
        {

            if (prodservice.RemoveProduct(Id))
                return Ok("Product Removed Successfully");
            return BadRequest("No Product To Delete");
        }

        [HttpGet]

        [Route("GetAllProductsNeedReview")]
        public IActionResult GetAllProductsNeedReview()
        {
            
                return Ok((adminService.GetAllProductsNeedReview()));
        }

        


    }
}