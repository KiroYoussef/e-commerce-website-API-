using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
namespace Electronic.Api.Services
{
    public interface ISubCategoryService
    {
        SubCatgoryDto GetSubCategoryById(int Id);
        IEnumerable<SubCategoryDTO> GetAllSubCategories();
        void AddNewSubCategory(SubCatgoryDto SubDTO);
        void UpdateSubCategory(int Id, SubCatgoryDto SubDTO);
        Task<bool> DeletSubCategory(int id);
    }
}
