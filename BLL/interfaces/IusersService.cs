using domain.Entities;
namespace BLL.interfaces
{
    public interface IusersService
    {
        public Task<string> GetUserRoleById(int id);

        public Task<User> GetUserFromConnectionAsync(string name, string password);

    }
}
