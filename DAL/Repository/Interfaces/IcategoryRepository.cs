using domain.Entities;

namespace DAL.Repository.Interfaces
{
    public interface IcategoryRepository
    {

        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategoryByReference(int reference);

    }
}
