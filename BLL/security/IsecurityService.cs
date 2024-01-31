namespace BLL.security
{
    public interface IsecurityService
    {
        public Task<string> signIn(string userName, string password);

    }
}
