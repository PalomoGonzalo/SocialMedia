using SocialMedia.Core.DTOS;

namespace SocialMedia.Core.Interfaces
{
    public interface ISeguridadRepositorio
    {
         Task<SeguridadDTO> ObtenerUsuarioSeguridad(UserLogin user);
         Task<SeguridadDTO> CrearUsuarioSeguridad(SeguridadDTO seguridadDTO);
         Task <string> Autenticacion(UserLogin user);
         Task<SeguridadDTO> GetUsuarioSeguridad(SeguridadDTO seguridad);

    }
}