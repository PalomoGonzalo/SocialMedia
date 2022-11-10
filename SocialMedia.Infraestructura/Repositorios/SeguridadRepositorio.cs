using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class SeguridadRepositorio : ISeguridadRepositorio
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioRepositorio _user;
       

        private readonly IPasswordHasherRepositorio _passwordHash;

        public SeguridadRepositorio(IConfiguration config, IUsuarioRepositorio user, IPasswordHasherRepositorio passwordHash)
        {
            _user = user;
            _config = config;
            _passwordHash = passwordHash;
        }

        public async Task<SeguridadDTO> CrearUsuarioSeguridad(SeguridadDTO seguridadDTO)
        {
            if (seguridadDTO == null)
                throw new ArgumentNullException();
            
            var passHash=_passwordHash.Hash(seguridadDTO.Contraseña);

            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string sql = @"INSERT INTO Seguridad (Usuario,NombreUsuario,Contraseña,Rol)
                            VALUES(@usuario,@nombreUsuario,@contraseña,@rol)";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("usuario", seguridadDTO.Usuario, DbType.String);
            dp.Add("nombreUsuario", seguridadDTO.NombreUsuario, DbType.String);
            dp.Add("contraseña", passHash, DbType.String);
            dp.Add("rol", seguridadDTO.Rol, DbType.Int64);



            int row = await db.ExecuteAsync(sql, dp);

            if (row ==0)
            {
                 throw new Exception("No se logro crear correctamente el usuario");
            }
            return seguridadDTO;
        }

        public async Task<SeguridadDTO> ObtenerUsuarioSeguridad(UserLogin user)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string query = $@"SELECT * FROM Seguridad WHERE Usuario=@user";

            DynamicParameters dp = new DynamicParameters();
            dp.Add("user", user.Usuario, DbType.String);

            return await db.QueryFirstOrDefaultAsync<SeguridadDTO>(query, dp);

        }

        public async Task<SeguridadDTO> GetUsuarioSeguridad(SeguridadDTO seguridad)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            string query = $@"SELECT * FROM Seguridad WHERE Usuario = @user";
            DynamicParameters dp = new DynamicParameters();
            dp.Add("user",seguridad.Usuario,DbType.String);
            return await db.QueryFirstOrDefaultAsync<SeguridadDTO>(query,dp).ConfigureAwait(false);


        }


        public async Task<string> Autenticacion(UserLogin user)
        {
            SeguridadDTO seguridadUser = await ObtenerUsuarioSeguridad(user);

            if (seguridadUser == null)
            {
                return null;
            }
            
            if(!(_passwordHash.CheckHash(seguridadUser.Contraseña, user.Password)))
            {
                return null;
            }

            string token= _user.GenerarToken(seguridadUser);
            return token;
        }
    }
}