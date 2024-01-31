using DAL.Sessions.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Sessions.Implementations;
internal class DBSessionMariaDB : IDBSession, IDisposable
{
    public IDbConnection Connection { get; private set; }

    public IDbTransaction Transaction { get; set; }
    public object users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public DBSessionMariaDB(string connectionString)
    {
        Connection = new MySqlConnection(connectionString);
        Connection.Open();
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}