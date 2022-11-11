using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Enumeraciones;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class LlavesRepositorio : ILlavesRepositorio
    {
        private readonly IConfiguration _config;

        public LlavesRepositorio(IConfiguration config)
        {
            _config = config;
        }

        public async Task CrearLLave(int usuario, EnumsLib.TipoLlave tipoLLave)
        {
            
            var llave = Guid.NewGuid().ToString().Replace("-","");
            var llaveApi= new LlaveApiDTO
            {
                Activo=true,
                Llave=llave,
                TipoLlave=tipoLLave,
                Usuario = usuario
            };
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"INSERT INTO LlaveApi (Llave,TipoLLave,Activo,Usuario)
                            VALUES(@llave,@tipoLlave,@activo,@usuario)";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("llave", llaveApi.Llave, DbType.String);
            dp.Add("tipoLlave", llaveApi.TipoLlave, DbType.Int16);
            dp.Add("activo", llaveApi.Activo, DbType.Boolean);
            dp.Add("usuario", llaveApi.Usuario, DbType.String);



            int row = await db.ExecuteAsync(sql, dp);

            if (row ==0)
            {
                 throw new Exception("No se logro crear correctamente el");
            }



            
           
        }


    }
}