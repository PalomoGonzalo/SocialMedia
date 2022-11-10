using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialMedia.Core.Enumeraciones.EnumsLib;

namespace SocialMedia.Core.DTOS
{
    public class LlaveApiDTO
    {
        public int Id { get; set; }
        public string Llave { get; set; }
        public TipoLlave TipoLlave { get; set; }
        public bool Activo { get; set; }    
        public string Usuario { get; set; }
    }
}