using domain.Entities;
namespace DAL.Repository.Interfaces
{
    public interface IuserRepository
    {
        public Task<string> getUserRoleById(int userId);
        public Task<IEnumerable<User>> GetAllUsersAsync();

        public Task<User> GetUserFromConnectionAsync(string name, string password);

    }
}
