namespace domain.Exceptions
{
    public class NotFoundEntityException : Exception
    {
        public NotFoundEntityException(string entityName, int id) : base($"L'entité {entityName} avec l'id: {id} est introuvable")
        {

        }
    }
}
