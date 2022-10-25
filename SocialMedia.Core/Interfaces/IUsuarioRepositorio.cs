using SocialMedia.Core.DTOS;
using SocialMedia.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUsuarioRepositorio
    {


        Task<UsuarioDTO> ObtenerUsuarioPorId(int id);
        Task<IEnumerable<UsuarioDTO>> ObtenerUsuario();
        String GenerarToken(UserLogin usuario);
    }
}
