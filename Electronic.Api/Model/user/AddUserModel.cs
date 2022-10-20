using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic.Api.Model.user
{
    public class AddUserModel
    {

        [StringLength(256), Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = String.Empty;

        [StringLength(256), Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;

        [Required]
        public bool EmailConfirmed { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = String.Empty;

        public string Address { get; set; } = String.Empty;
    }
}
