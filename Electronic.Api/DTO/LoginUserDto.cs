using System.ComponentModel.DataAnnotations;

namespace Electronic.Api.DTO
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
       // public bool RememberMe { get; set; }
    }
}
