using Electronic.Api.DTO;
using Electronic.Api.Dtos;
using Electronic.Api.Model;
using Electronic.Api.Repository;

namespace Electronic.Api.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IBaseRepository<SubCategory> _SubCategoryRepositroy;
        private readonly IBaseRepository<Category> _CategoryRepositroy;
        public SubCategoryService(IBaseRepository<SubCategory> repo, IBaseRepository<Category> categoryRepositroy)
        {
            _SubCategoryRepositroy = repo;
            _CategoryRepositroy = categoryRepositroy;
        }
        public void AddNewSubCategory(SubCatgoryDto SubDTO)
        {
            var SubCategory = new SubCategory();
            SubCategory.name = SubDTO.name;
            SubCategory.CategoryID = SubDTO.CategoryID;
            _SubCategoryRepositroy.Insert(SubCategory);
        }

        public IEnumerable<SubCategoryDTO> GetAllSubCategories()
        {
            var SubCats = _SubCategoryRepositroy.GetAll();
            if (SubCats != null && SubCats.Count() > 0)
            {
                List<SubCategoryDTO> SubCatDTOs = new List<SubCategoryDTO>();
                foreach (var sub in SubCats)
                {

                    SubCatDTOs.Add(
                        new SubCategoryDTO
                        {
                            Id = sub.Id,
                            name = sub.name,
                            CategoryName = _CategoryRepositroy.GetById(sub.CategoryID).name,
                            CategoryID=sub.CategoryID,
                        }
                        );
                }
                return SubCatDTOs;
            }
            return null;
        }

        public SubCatgoryDto GetSubCategoryById(int Id)
        {
            var SubCat = _SubCategoryRepositroy.GetById(Id);
            if (SubCat != null)
                return new SubCatgoryDto { Id = SubCat.Id, name = SubCat.name, CategoryID = SubCat.CategoryID };
            return null;
        }

        public async Task<bool> DeletSubCategory(int id)
        {
            var model = _SubCategoryRepositroy.GetById(id);

            if (model == null)
            {
                return false;
            }
            _SubCategoryRepositroy.Delete(model);
            return true;
        }


        public void UpdateSubCategory(int Id, SubCatgoryDto SubDTO)
        {
            var SubCat = _SubCategoryRepositroy.GetById(Id);
            if (SubCat != null)
            {
                SubCat.name = SubDTO.name;
                SubCat.CategoryID = SubDTO.CategoryID;
                _SubCategoryRepositroy.Update(SubCat);
            }
        }

    }
}
