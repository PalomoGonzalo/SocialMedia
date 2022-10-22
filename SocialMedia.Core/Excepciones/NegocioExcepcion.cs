using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Excepciones
{
    public class NegocioExcepcion : Exception
    {
        public NegocioExcepcion(string message) : base(message)
        {
        }
    }
}
