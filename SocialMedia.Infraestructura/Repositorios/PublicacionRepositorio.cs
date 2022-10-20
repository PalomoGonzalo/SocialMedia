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

        public async Task<int> CrearPublicacion(PublicacionCreacionDTO publicacionCreacionDTO)
        {
            if (publicacionCreacionDTO == null)
                throw new ArgumentNullException();

            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"INSERT INTO Publicacion (IdUsuario,Fecha,Descripcion,Imagen)
                            VALUES(@idUsuario,@fecha,@descripcion,@imagen)";

            DynamicParameters dp=new DynamicParameters();
            dp.Add("idUsuario", publicacionCreacionDTO.IdUsuario,DbType.Int64);
            dp.Add("fecha", DateTime.Now,DbType.DateTime);
            dp.Add("descripcion", publicacionCreacionDTO.Descripcion,DbType.String);
            dp.Add("imagen", "null",DbType.String);

            int row = await db.ExecuteAsync(sql, dp);

            if(row > 0)
            {
                return row;
            }
            return -1;
        }

        public async Task<int> ModificarComentarioPublicacion(string comentario, int id)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            DynamicParameters dp = new DynamicParameters();
            string sql = @$"UPDATE Publicacion SET Descripcion = @descripcion WHERE IdPublicacion=@id";
            dp.Add("descripcion", comentario, DbType.String);
            dp.Add("id",id,DbType.Int64);

            int row = await db.ExecuteAsync(sql, dp);  

            if(row>0)
                return row;
            return -1;
        }
    }
}
