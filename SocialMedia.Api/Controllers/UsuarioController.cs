using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        public IUsuarioRepositorio _usuarioRepositorio { get; }
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            
        }


        [HttpGet("ObtenerUsuarioPorId/{id}")]       
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var usuario= await _usuarioRepositorio.ExisteUsuarioPorId(id);
            if(usuario==null)
            {
                return NotFound("No existe el usuario");
            }
            return Ok(usuario);
        }

        [HttpGet("obtenerUsuarios")]

        public async Task<IActionResult> OtenerUsuarios()
        {
            var listUsuario= await _usuarioRepositorio.ObtenerUsuario();
            if (listUsuario==null)
            {
                return NotFound("no existe el usuario");
            }
            return Ok(listUsuario);
        }

    }
}