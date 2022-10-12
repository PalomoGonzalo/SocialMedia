using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructura.Repositorios
{
    public class PublicacionRepositorio : IPublicacionRepositorio
    {
        public Task<IEnumerable<Publicacion>> GetPublicaciones()
        {
            throw new NotImplementedException();
        }
    }
}
