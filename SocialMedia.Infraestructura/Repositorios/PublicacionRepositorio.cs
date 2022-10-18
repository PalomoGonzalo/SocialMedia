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
    public class PublicacionRepositorio : IPublicacionRepositorio
    {
        private readonly IConfiguration _config;

        public PublicacionRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task<PublicacionDTO> GetPublicacion(int id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            if(db.State==ConnectionState.Closed)
                db.Open();

            string sql = @"SELECT * FROM Publicacion where IdPublicacion = @id";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("id", id, DbType.Int64);

            var publicacion = await db.QueryFirstOrDefaultAsync<PublicacionDTO>(sql,dp);

            return publicacion;
        }

        public async Task<IEnumerable<PublicacionDTO>> GetPublicaciones()
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"SELECT * FROM Publicacion ";

            IEnumerable<PublicacionDTO> lista = await db.QueryAsync<PublicacionDTO>(sql).ConfigureAwait(false);
            
            return lista;
 
        }
    }
}
