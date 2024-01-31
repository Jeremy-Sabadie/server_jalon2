using DAL.Repository.Interfaces;
using DAL.Sessions.Interfaces;
using Dapper;
using domain.Entities;

namespace DAL.Repository.Implémentations
{
    internal class UserRepository : IuserRepository
    {
        private readonly IDBSession _db;

        public UserRepository(IDBSession dBSession)
        {
            _db = dBSession;
        }

        public async Task<string> getUserRoleById(int userId)
        {
            string request = @"
            SELECT role 
            FROM users 
            WHERE id = @userId";
            var role = await _db.Connection.ExecuteScalarAsync<string>(request, new { userId });
            return role;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            string request = @"
            SELECT* 
            FROM users ";
            var allUsersResult = await _db.Connection.QueryAsync<User>(request);
            return allUsersResult;
        }
        public async Task<User> GetUserFromConnectionAsync(string name, string password)
        {   //Sélection de toutes les données de l'utilisateur quand l'identifiant entré correspond à la colone name.
            string request = @"SELECT * FROM users WHERE email=@name";
            return await _db.Connection.QueryFirstAsync<User>(request, new { name });
        }

    }
}
