using SocialMedia.Core.DTOS;
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
        Task< IEnumerable<PublicacionDTO>> GetPublicaciones(int pagina, int cantidadRegistros);
        Task<PublicacionDTO> GetPublicacion(int id);
        Task<int> CrearPublicacion(PublicacionCreacionDTO publicacionCreacionDTO);
        Task<int> ModificarDescripcionPublicacion(string comentario,int id);
        Task<IEnumerable<PublicacionCantidadDTO>> ObtenerCantidadDepubicacionesPorUsuario();
        Task<int> ObtenerCantidadDepubicacionesPorUsuarioID(int id);
        Task<int> ObtenerCantidadDePaginas(int cantidadRegistros);

    }
}
