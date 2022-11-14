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
        public int IdUsuario { get; set; }


        public string Descripcion { get; set; } = null!;

        public string Imagen { get; set; }
    }
}
