using System.Data;

namespace DAL.Sessions.Interfaces;

public interface IDBSession : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; set; }
    object users { get; set; }
}
