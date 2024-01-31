using DAL.Repository.Interfaces;

namespace DAL.UOW
{
    public interface IUOW : IDisposable
    {

        //Repositories:
        ImaterielsRepository Materiels { get; }
        IcategoryRepository Categories { get; }
        IuserRepository Users { get; }

        //Transactions
        /// <summary>
        /// Begin a transaction on the current connection
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commit the current transaction
        /// </summary>
        void Commit();
        /// <summary>
        /// Rollback the current transaction
        /// </summary>
        void Rollback();

    }

}
