using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic.Api.Model.user
{
    public class UserRolesModel
    {

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
