using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.DTOS
{
    public class LlaveApiListadoDTO
    {
        public string Llave { get; set; }
        public bool Activo { get; set; }
        public string TipoLlave { get; set; }
    }
}