using SocialMedia.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPublicacionRepositorio
    {
        Task< IEnumerable<Publicacion>> GetPublicaciones();
    }
}
