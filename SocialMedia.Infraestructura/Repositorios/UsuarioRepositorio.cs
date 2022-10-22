using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            return await db.QueryFirstOrDefaultAsync<UsuarioDTO>(query, dp);

        }

        public async Task<IEnumerable<UsuarioDTO>> ObtenerUsuario()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            string query = $"SELECT * FROM Usuario";

            IEnumerable<UsuarioDTO> usuarios = await db.QueryAsync<UsuarioDTO>(query).ConfigureAwait(false);

            return usuarios;

        }
    }
}
