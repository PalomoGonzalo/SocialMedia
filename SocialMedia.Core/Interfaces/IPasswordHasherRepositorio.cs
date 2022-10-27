namespace SocialMedia.Core.Interfaces
{
    public interface IPasswordHasherRepositorio
    {
         string Hash(string password);
         bool CheckHash(string hash, string password);
    }
}