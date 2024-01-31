using DAL.Repository.Interfaces;
using DAL.Sessions.Interfaces;
using Dapper;
using domain.Entities;

namespace DAL.Repository.Implémentations
{
    internal class CategoryRepository : IcategoryRepository
    {
        private readonly IDBSession _db;

        public CategoryRepository(IDBSession dBSession)
        {
            _db = dBSession;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            string allCategoriesRequest = "select * from category ";
            return await _db.Connection.QueryAsync<Category>(allCategoriesRequest, _db.Transaction);

        }
        public async Task<Category> GetCategoryByReference(int reference)
        {
            string request = "select* from category where reference = @reference";
            return await _db.Connection.QueryFirstOrDefaultAsync<Category>(request, new { reference });
        }
    }
}
