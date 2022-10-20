using Electronic.Api.DTO;
using Electronic.Api.Model;

namespace Electronic.Api.Services
{
    public interface IUserService
    {
        string GetUserNameByID(string Id);
        string GetUserPhotoByID(string Id);
        List<SellerDTO> GetAllSeller();
        IEnumerable<ApplicationUser> GetAllUser();
        Task<ApplicationUser> GetUser(string userId);
    }
}
