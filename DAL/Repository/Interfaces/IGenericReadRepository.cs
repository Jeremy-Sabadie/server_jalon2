using domain.Entities;

namespace DAL.Repository.Interfaces
{
    internal interface IGenericReadRepository<U, T> where T : IEntity
    {
        public Task<T> GetByIdAsync(U id);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
