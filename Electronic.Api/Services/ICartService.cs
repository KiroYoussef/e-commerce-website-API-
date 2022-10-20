using Electronic.Api.DTO;

namespace Electronic.Api.Services
{
    public interface ICartService
    {
        public IEnumerable<CartProductDTO> GetAll(string iduser);
        public bool AddCart(string iduser, int idprodect);
        public bool CheckAny(string iduser, int idprodect);
        public int CountCart(string iduser);
        public bool AddCountProduct(string iduser, int idprodect, int pluscount);
        public bool DeleteCartProduct(string iduser, int idprodect);

        public bool ChangeQuentityAndCheckMax(string iduser, int idprodect);
        public bool ChangeQuentityAndCheckMax(string iduser, int idprodect, int count);

        public bool AddCartByDetails(string iduser, int idprodect, int count);
    }
}
