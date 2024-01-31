using domain.Entities;

namespace domain.Exceptions
{
    public class DeleteEntityException : Exception
    {
        public DeleteEntityException(IEntity entity, int id) : base($"La suppression de {nameof(entity)} avec id : {id} dans la base de données a échoué") { }
    }
}
