using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Enumeraciones
{
    public class EnumsLib
    {
        public enum CantidadPublicaciones
        {
            Max=10
        }

        public enum RolType
        {
            Administrador,
            Consumidor
        }

        public enum TipoLlave
        {
            Gratuita = 1,
            Profesional = 2
        }
    }
}
