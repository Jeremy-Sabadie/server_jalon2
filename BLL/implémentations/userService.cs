using BLL.interfaces;
using DAL.UOW;
using domain.Entities;

namespace BLL.implémentations
{
    internal class userService : IusersService
    {
        private readonly IUOW _db;

        public userService(IUOW db)
        {
            _db = db;
        }
        public async Task<string> GetUserRoleById(int id)
        {
            return await _db.Users.getUserRoleById(id);
        }
        public async Task<User> GetUserFromConnectionAsync(string name, string password)
        {
            return await _db.Users.GetUserFromConnectionAsync(name, password);
        }
    }
}
