using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Model.user;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanager;
        private readonly ContextDB contextDB;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> usermanager, ContextDB contextDB,
                                 IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.usermanager = usermanager;
            this.contextDB = contextDB;
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        // create account new user "Registeration" "Post"
        [HttpPost("register")]  // api/account/register
        public async Task<IActionResult> Registeration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userExists = await usermanager.FindByNameAsync(userDto.UserName);
                    if (userExists != null)
                    {
                        return BadRequest("User Already Exists !");
                    }



                    var emailExists = await usermanager.FindByEmailAsync(userDto.Email);
                    if (emailExists != null)
                    {
                        return BadRequest("Email Already Exists !");
                    }
                    //save

                    ApplicationUser user = new ApplicationUser();
                    user.UserName = userDto.UserName;
                    user.Email = userDto.Email;
                    user.Address = userDto.Address;
                    user.PhoneNumber = userDto.Phone;
                    user.Img = userDto.Image;
                    IdentityResult result = await usermanager.CreateAsync(user, userDto.Password);
                    if (result.Succeeded)
                    {
                        if (userDto.RoleName.ToLower() == "Seller".ToLower())
                        {

                            await usermanager.AddToRoleAsync(user, Enums.Roles.Seller.ToString());
                        }
                        else
                        {
                            await usermanager.AddToRoleAsync(user, Enums.Roles.User.ToString());

                        }
                        return Ok("Account Added Successfully");

                    }
                    else
                    {
                        foreach (var res in result.Errors)
                        {
                            return BadRequest($"Errors are : {res.Description} -- ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest(ModelState);
        }

        //check Account valid "Login" "Post"
        [HttpPost("login")] // api/account/login
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // check - create token
                    ApplicationUser user = await usermanager.FindByEmailAsync(userDto.Email);
                    if (user != null) //username found
                    {
                        bool found = await usermanager.CheckPasswordAsync(user, userDto.Password);
                        if (found)
                        {
                            //Claims token
                            var claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                            claims.Add(new Claim(ClaimTypes.Email, user.Email));
                            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                            //get role
                            var roles = await usermanager.GetRolesAsync(user);
                            foreach (var itemRole in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, itemRole));
                            }

                            SecurityKey securityKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                            SigningCredentials signingCred =
                                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                            // Create Token
                            JwtSecurityToken mytoken = new JwtSecurityToken
                                (
                                    issuer: configuration["JWT:ValidIssuer"], // url web api
                                    audience: configuration["JWT:ValidAudience"], // url consumer angular
                                    claims: claims,
                                    expires: DateTime.Now.AddHours(1),
                                    signingCredentials: signingCred
                                );
                            return Ok(new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration = mytoken.ValidTo,
                                role = roles.FirstOrDefault(),
                                img = "https://localhost:7096/Img/Users/" + user.Img,
                                nameUser = user.UserName,
                                id = user.Id
                            });
                        }
                        return Unauthorized();
                    }
                    return Unauthorized();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Unauthorized();

        }


        [HttpPut, Route("UploadIMGUser")]
        public async Task<IActionResult> UploadIMGUser()
        {
            try
            {
                var UserId = HttpContext.Request.Form["userId"].ToString();
                var img = HttpContext.Request.Form.Files["image"];
                if (img == null)
                {
                    return BadRequest();
                }

                ApplicationUser user = await usermanager.FindByIdAsync(UserId);
                user.Img = UploadFile(img);
                await contextDB.SaveChangesAsync();
                return Ok("https://localhost:7096/Img/Users/" + user.Img);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }


        private string UploadFile(IFormFile Image)
        {
            string fileName = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img/Users");

                fileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            return fileName;


        }
    }
}