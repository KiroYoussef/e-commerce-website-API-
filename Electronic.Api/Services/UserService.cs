using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Electronic.Api.Repository.Custom_Repository;
using Microsoft.AspNetCore.Identity;

namespace Electronic.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ContextDB context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBaseRepository<Product> productRepository;

        private readonly IBaseRepository<ApplicationUser> _UserRepositroy;

        public UserService(UserManager<ApplicationUser> userManager, IProductRepository product2addRepository, IBaseRepository<ApplicationUser> repo, ContextDB _contextDB)
        {
            this.userManager = userManager;
            this.productRepository = product2addRepository;

            context = _contextDB;
            _UserRepositroy = repo;

        }
        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return _UserRepositroy.GetAll();

        }
        public async Task<ApplicationUser> GetUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            user.Img = "https://localhost:7096/img/Users/" + user.Img;
            return user;
        }
        public string GetUserNameByID(string userId)
        {
            var name = context.Users.Where(e => e.Id == userId).Select(d => d.UserName).First();
            return name;
        }
        public string GetUserPhotoByID(string userId)
        {
            var img = context.Users.Where(e => e.Id == userId).Select(d => d.Img).First();
            return img;
        }
        public List<SellerDTO> GetAllSeller()
        {
            var RoleSellerId = context.Roles.Where(p => p.NormalizedName == "SELLER").Select(R => R.Id).FirstOrDefault();
            var Sellers = context.UserRoles.Where(e => e.RoleId == RoleSellerId).Select(d => d.UserId).ToList();
            List<SellerDTO> AllSeller = new List<SellerDTO>();
            foreach (var seller in Sellers)
            {

                AllSeller.Add(new SellerDTO
                {
                    name = context.Users.Where(o => o.Id == seller).Select(o => o.UserName).First(),
                    Id = context.Users.Where(o => o.Id == seller).Select(o => o.Id).First(),
                    img = "https://localhost:7096/img/Users/" + context.Users.Where(o => o.Id == seller).Select(o => o.Img).First(),
                    ProductCount = productRepository.Count(R => R.UserID == context.Users.Where(o => o.Id == seller).Select(o => o.Id).First()),
                });

            }
            return AllSeller;
        }

    }
}
