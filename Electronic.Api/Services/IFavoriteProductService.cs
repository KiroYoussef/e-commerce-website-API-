using Electronic.Api.DTO;
using Electronic.Api.Model;

namespace Electronic.Api.Reporsitories
{
    public interface IFavoriteProductService
    {
        List<FavoriteProductsDTO> GetAllByID(string UserId);
        void AddNew(User_ProductDTO  UserP);
        CheckFavoProd_ProdIdDTO checkProduct(User_ProductDTO UserP);
        int count(string Userid);

        void delete(int favoID);


    }
}
