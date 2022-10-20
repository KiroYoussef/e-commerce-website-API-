using Electronic.Api.Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Electronic.Api.Repository;
using Electronic.Api.DTO;

namespace Electronic.Api.Reporsitories
{
    public class FavoriteProductService : IFavoriteProductService
    {
        private readonly ContextDB context;
        private readonly IBaseRepository<Product> productRepository;

        private readonly IBaseRepository<FvoriteProduct> _CommentRepositroy;

        public FavoriteProductService(IBaseRepository<FvoriteProduct> repo, ContextDB _contextDB,
            IBaseRepository<Product> productRepository)
        {
            context = _contextDB;
            _CommentRepositroy = repo;
            this.productRepository = productRepository;
        }


        public void AddNew(User_ProductDTO UserP)

        {
            var fp = new FvoriteProduct()
            {
                ProductID = UserP.ProductId,
                UserID = UserP.UserId
            };
            _CommentRepositroy.Insert(fp);
        }
        public void delete(int favoID)

        {
            var Entiti = _CommentRepositroy.GetById(favoID);
            _CommentRepositroy.Delete(Entiti);
        }


        public List<FavoriteProductsDTO> GetAllByID(string UserId)
        {
            var productsID = context.FvoriteProducts.Where(m => m.UserID == UserId).Select(m => m.ProductID).ToList();
            var FavoID = context.FvoriteProducts.Where(m => m.UserID == UserId).Select(m => m.iD).ToList();

            List<FavoriteProductsDTO> FavoriteProducts = new List<FavoriteProductsDTO>();
            int i = 0;
            foreach (var ID in productsID)
            {
                var PRO = productRepository.GetById(ID);
                var SubCategoryName = context.SubCategories.FirstOrDefault(f => f.Id == PRO.SubCategoryID);
                FavoriteProducts.Add(new FavoriteProductsDTO
                {
                    FavoriteID = FavoID[i],
                    Id = PRO.Id,
                    name = PRO.name,
                    Description = PRO.Description,
                    Discount = PRO.Discount,
                    price = PRO.price,
                    img = "https://localhost:7096/img/Products/" + PRO.img,
                    SubCategoryName = SubCategoryName.name
                });
                i++;
            }
            return FavoriteProducts;
            ;
        }
        public CheckFavoProd_ProdIdDTO checkProduct(User_ProductDTO UserP)
        {
            var check = context.FvoriteProducts.Where(m => m.UserID == UserP.UserId).Select(m => m.ProductID == UserP.ProductId).FirstOrDefault();
            var productID = context.FvoriteProducts.FirstOrDefault(P => P.UserID == UserP.UserId && P.ProductID == UserP.ProductId);
            if (productID != null)
            {
                return new CheckFavoProd_ProdIdDTO() { Existing = check, ProdID = productID.iD };

            }
            return new CheckFavoProd_ProdIdDTO() { Existing = false, ProdID = 0 };

        }
        public int count(string Userid)
        {
            var favoProduct = context.FvoriteProducts.Where(m => m.UserID == Userid).ToList();
            return favoProduct.Count();
        }


    }
}
