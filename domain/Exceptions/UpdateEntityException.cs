using domain.Entities;

namespace domain.Exceptions
{
    public class UpdateEntityException : Exception
    {
        public UpdateEntityException(IEntity entity) : base($"La modification de {nameof(entity)} dans la base de données a échoué") { }
    }
}
