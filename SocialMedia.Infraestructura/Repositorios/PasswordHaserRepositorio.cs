using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Options;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class PasswordHaserRepositorio : IPasswordHasherRepositorio
    {
        private readonly PassOptions _config;
        public PasswordHaserRepositorio(IOptions<PassOptions> config)
        {
            _config = config.Value;
        }


        public bool CheckHash(string hash, string password)
        {
            var parts = hash.Split('.',3);
            if (parts.Length!=3)
            {
                throw new FormatException("Inesperado formato de has");
            }

            var iteraciones = Convert.ToInt32(parts[0]) ;
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algorithm = new Rfc2898DeriveBytes(
             password,
             salt,
             iteraciones
            ))
            {
                var keyToCheck = algorithm.GetBytes(_config.KeySize);
                return keyToCheck.SequenceEqual(key);
            }


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
                var key = Convert.ToBase64String(algorithm.GetBytes(_config.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_config.Iteraciones}.{salt}.{key}";
            }
        }
    }
}