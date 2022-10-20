using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Model.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic.Api.Services
{
    public interface IAdminService
    {
        public CountAdminDTO CountAdmin();
        Task<AuthModel> AddUserAsync(AddUserModel model);
        Task<AuthModel> EditUserAsync(EditUserModel model);
        Task<bool> DeleteUsersAsync(List<string> model);
        Task<IEnumerable<UserRolesModel>> GetUserRoleAsync();
        Task<bool> EditUserRoleAsync(EditUserRoleModel model);





    }
}
