using static SocialMedia.Core.Enumeraciones.EnumsLib;

namespace SocialMedia.Core.DTOS
{
    public class SeguridadDTO
    {
        public String Usuario { get; set; }
        public String Contrase�a { get; set; }
        public String NombreUsuario { get; set; }
        public RolType Rol { get; set;}
    }
}