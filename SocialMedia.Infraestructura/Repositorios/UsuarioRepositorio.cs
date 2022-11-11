using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IConfiguration _config;

        public UsuarioRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task<UsuarioDTO> ObtenerUsuarioPorId(int id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string query = $"SELECT * FROM Usuario WHERE IdUsuario=@id";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("id", id,DbType.Int64);

            var user= await db.QueryFirstOrDefaultAsync<UsuarioDTO>(query, dp);

            return user;

        }

        public async Task<IEnumerable<UsuarioDTO>> ObtenerUsuario()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            string query = $"SELECT * FROM Usuario";

            IEnumerable<UsuarioDTO> usuarios = await db.QueryAsync<UsuarioDTO>(query).ConfigureAwait(false);

            return usuarios;

        }


        public String GenerarToken(SeguridadDTO seguridad)
        {
            JwtSecurityTokenHandler tokenhandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("usuario", seguridad.IdUsuario.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretKey"])), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken token = tokenhandler.CreateToken(tokenDescriptor);
            String encodeJwt = tokenhandler.WriteToken(token);

            return encodeJwt;
        }
    }
}
