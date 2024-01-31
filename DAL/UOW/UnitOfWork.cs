using DAL.Repository.Implémentations;
using DAL.Repository.Interfaces;
using DAL.Sessions.Interfaces;


namespace DAL.UOW;
public class UnitOfWork : IUOW
{
    private readonly IDBSession db;

    public UnitOfWork(IDBSession dBSession)
    {
        db = dBSession;
    }

    public ImaterielsRepository Materiels => new MaterielsRepository(db);

    public IcategoryRepository Categories => new CategoryRepository(db);
    public IuserRepository Users => new UserRepository(db);


    public void BeginTransaction()
    {
        if (db.Transaction is null)
        {
            db.Transaction = db.Connection.BeginTransaction();
        }
    }

    public void Commit()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Commit();
            db.Transaction = null;
        }
    }

    public void Dispose()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Dispose();
            db.Transaction = null;
        }
        db.Connection.Dispose();
    }

    public void Rollback()
    {
        if (db.Transaction is not null)
        {
            db.Transaction.Rollback();
            db.Transaction = null;
        }
    }
}