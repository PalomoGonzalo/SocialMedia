using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTOS
{
    public class PublicacionCreacionDTO
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Descripcion { get; set; } = null!;

        [Required]
        public string Imagen { get; set; }
    }
}
