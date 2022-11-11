using static SocialMedia.Core.Enumeraciones.EnumsLib;

namespace SocialMedia.Core.DTOS
{
    public class SeguridadDTO
    {
        public int  IdUsuario { get; set; }
        public String Contraseña { get; set; }
        public String NombreUsuario { get; set; }
        public RolType Rol { get; set;}
    }
}