namespace SharedHome.Infrastructure.Identity.Services
{
    public interface IPasswordHashService
    {
        byte[] GetSecureSalt();

        string HashUsingPbkdf2(string password, byte[] salt);
    }
}
