using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTOS
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
    
    }
}