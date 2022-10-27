using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Options;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class PasswordHaserRepositorio : IPasswordHasherRepositorio
    {
        private readonly PasswordOptions _config;
        public PasswordHaserRepositorio(PasswordOptions config)
        {
            _config = config;
        }
        public bool CheckHash(string hash, string password)
        {

            
        }

        public string Hash(string password)
        {
            //PBKDF2 IMPLEMENTACION
           using (var algorithm = new Rfc2898DeriveBytes(
            password,
            _config.SaltSize,
            _config.Iteraciones
           ))
           {
            var key = 
           }
            
           
        }
    }
}