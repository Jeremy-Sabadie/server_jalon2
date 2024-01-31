using domain.Entities;

namespace domain.Exceptions
{
    public class InsertEntityException : Exception
    {
        public InsertEntityException(IEntity entity) : base($"L'insert de {nameof(entity)} dans la base de données a échoué") { }
    }

}
