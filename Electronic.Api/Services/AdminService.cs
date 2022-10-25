using AutoMapper;
using AutoMapper.Configuration;
using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Model.user;
using Electronic.Api.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Electronic.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly IBaseRepository<Order> orderRepository;
        private readonly IBaseRepository<SubCategory> subcategoryRepository;
        private readonly IBaseRepository<Category> categoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IBaseRepository<ApplicationUser> genericRepository;
        private readonly ContextDB db;

        private readonly IMapper mapper;
        private readonly IBaseRepository<Product> product;

        public AdminService(IBaseRepository<Order> OrderRepository, IBaseRepository<SubCategory> subcategoryRepository, IBaseRepository<Category> categoryRepository, UserManager<ApplicationUser> userManager, IBaseRepository<ApplicationUser> genericRepository, ContextDB db,
             IMapper mapper, IBaseRepository<Product> product)
        {
            orderRepository = OrderRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.categoryRepository = categoryRepository;
            this._userManager = userManager;
            this.genericRepository = genericRepository;
            this.db = db;
            this.mapper = mapper;
            this.product = product;
        }

        public CountAdminDTO CountAdmin()
        {
            var RoleSellerId = db.Roles.Where(p => p.Name == Enums.Roles.Seller.ToString()).Select(R => R.Id).FirstOrDefault();
            var Sellers = db.UserRoles.Where(e => e.RoleId == RoleSellerId).Select(d => d.UserId).ToList();

            var RoleUsersId = db.Roles.Where(p => p.Name == Enums.Roles.User.ToString()).Select(R => R.Id).FirstOrDefault();
            var Userss = db.UserRoles.Where(e => e.RoleId == RoleUsersId).Select(d => d.UserId).ToList();

            CountAdminDTO model = new CountAdminDTO();
            model.Sellers = Sellers.Count();
            model.Users = Userss.Count();
            model.Orders = orderRepository.GetAll().Count();

            model.Products = product.GetAll().Count();

            return model;
        }


        public async Task<AuthModel> AddUserAsync(AddUserModel model)
        {



            if (model == null)
            {
                return null;
            }


            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return new AuthModel { Message = "User Already Exists !" };

            }



            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
            {
                return new AuthModel { Message = "Email Already Exists !" };

            }


            var user = mapper.Map<ApplicationUser>(model);
            user.Img = "No-Image.png";

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };

            }
            await _userManager.AddToRoleAsync(user, Enums.Roles.User.ToString());

            return new AuthModel { Message = "sucessful" };

        }

        public async Task<AuthModel> EditUserAsync(EditUserModel model)
        {
            var user = genericRepository.GetByIds(model.Id);
            if (model.Password != user.PasswordHash)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, model.Password);
                }
            }
            genericRepository.Update(user);


            user.Email = model.Email;
            user.UserName = model.UserName;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            db.SaveChanges();

            return new AuthModel { Message = "تم التعديل" };

        }

        public async Task<bool> DeleteUsersAsync(List<string> model)
        {
            if (model == null)
            {
                return false;
            }
            int i = 0;
            foreach (var mod in model)
            {
                var user = genericRepository.GetByIds(mod);
                if (user != null)
                {
                    genericRepository.Delete(user);
                    i++;
                }

            }
            if (i > 0)
            {

                return true;
            }
            return false;

        }


        public async Task<IEnumerable<UserRolesModel>> GetUserRoleAsync()
        {
            var query = await (
              from userRole in db.UserRoles
              join users in db.Users
              on userRole.UserId equals users.Id
              join roles in db.Roles
              on userRole.RoleId equals roles.Id
              select new
              {
                  userRole.UserId,
                  users.UserName,
                  userRole.RoleId,
                  roles.Name
              }).ToListAsync();

            List<UserRolesModel> userRolesModels = new List<UserRolesModel>();
            if (query.Count > 0)
            {
                for (int i = 0; i < query.Count; i++)
                {
                    var model = new UserRolesModel
                    {
                        UserId = query[i].UserId,
                        UserName = query[i].UserName,
                        RoleId = query[i].RoleId,
                        RoleName = query[i].Name
                    };
                    userRolesModels.Add(model);
                }
            }
            return userRolesModels;
        }


        public async Task<bool> EditUserRoleAsync(EditUserRoleModel model)
        {
            if (model.UserId == null || model.RoleId == null)
            {
                return false;
            }

            var user = genericRepository.GetByIds(model.UserId);
            if (user == null)
            {
                return false;
            }

            var currentRoleId = await db.UserRoles.Where(x => x.UserId == model.UserId).Select(x => x.RoleId).FirstOrDefaultAsync();
            var currentRoleName = await db.Roles.Where(x => x.Id == currentRoleId).Select(x => x.Name).FirstOrDefaultAsync();
            var newRoleName = await db.Roles.Where(x => x.Id == model.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

            if (await _userManager.IsInRoleAsync(user, currentRoleName))
            {
                var x = await _userManager.RemoveFromRoleAsync(user, currentRoleName);
                if (x.Succeeded)
                {
                    var s = await _userManager.AddToRoleAsync(user, newRoleName);
                    if (s.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public IEnumerable<ProductsGetALLDTO> GetAllProducts()
        {

            List<ProductsGetALLDTO> productDTOs = new List<ProductsGetALLDTO>();
            //var products = db.Products;
            foreach (Product p in db.Products)
            {
                ProductsGetALLDTO productsGet = new ProductsGetALLDTO();

                productsGet.Id = p.Id;
                productsGet.price = p.price;
                productsGet.name = p.name;
                productsGet.userId = p.UserID;
                productsGet.priceAfterDisc = p.price + (p.price * p.Discount / 100);
                productsGet.img = "https://localhost:7096/Img/Products/" + p.img;
                productsGet.Discount = p.Discount;
                productsGet.SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name;
                productsGet.active = p.Active;
                productDTOs.Add(productsGet);
            }
            return productDTOs;
        }
        public IEnumerable<ProductsGetALLDTO> GetAllProductsNeedReview()
        {
            var prods = product.GetAll().Where(j=>j.FirstAprove==0);
            List<ProductsGetALLDTO> AllProdDTOs = new List<ProductsGetALLDTO>();
            if (prods != null && prods.Count() > 0)
            {
                foreach (Product prod in prods)
                {

                    AllProdDTOs.Add(new ProductsGetALLDTO
                    {
                        Id = prod.Id,
                        price = prod.price,
                        name = prod.name,
                        priceAfterDisc = prod.price + (prod.price * prod.Discount / 100),
                        img = "https://localhost:7096/Img/Products/" + prod.img,
                        Discount = prod.Discount,
                        SubCategoryName = subcategoryRepository.GetById(prod.SubCategoryID).name,
                        FirstAprove = prod.FirstAprove

                    });
                }
                return AllProdDTOs;
            }
            return null;
        }
        public bool UpdateProductApprove(int ProdId, int firstApprove)
        {
            var Prod = product.GetById(ProdId);
            if (Prod != null)
            {
                Prod.FirstAprove = firstApprove;
                product.Update(Prod);
                return true;
            }
            return false;
        }

        public bool RemoveProduct(int Id)
        {
            var prod = product.GetById(Id);
            if (prod != null)
            {
                //prod.FirstAprove = false;
                //product.Update(prod);
                product.Delete(prod);
                return true;
            }
            return false;
        }
        //public IEnumerable<ProductsGetALLDTO> GetAllProducts()
        //{
        //    List<ProductsGetALLDTO> productDTOs = new List<ProductsGetALLDTO>();
        //    var products = product.GetAllwhere(e => e.FirstAprove == 1 && e.Active == true);
        //    foreach (Product p in products)
        //    {

        //        ProductsGetALLDTO productsGet = new ProductsGetALLDTO();

        //        productsGet.Id = p.Id;
        //        productsGet.price = p.price;
        //        productsGet.name = p.name;
        //        productsGet.priceAfterDisc = p.price + (p.price * p.Discount / 100);
        //        productsGet.img = "https://localhost:7096/Img/Products/" + p.img;
        //        productsGet.Discount = p.Discount;
        //        productsGet.FirstAprove = p.FirstAprove;
        //        productsGet.SubCategoryName = subcategoryRepository.GetById(p.SubCategoryID).name;

        //        //productsGet.FavoriteProductID = (context.FvoriteProducts.Where(o => o.UserID == UserId).Select(u => u.ProductID == p.Id).FirstOrDefault() == true) ? (context.FvoriteProducts.Where(o => o.UserID == UserId && o.ProductID == p.Id).Select(u => u.iD).FirstOrDefault()) : -1;
        //        productDTOs.Add(productsGet);
        //    }
        //    return productDTOs;
        //}




    }
}
