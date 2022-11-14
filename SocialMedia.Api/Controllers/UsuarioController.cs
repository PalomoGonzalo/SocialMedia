using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Interfaces;
using static SocialMedia.Core.Enumeraciones.EnumsLib;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISeguridadRepositorio _seguridad;
        private readonly ILlavesRepositorio _llave;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ISeguridadRepositorio seguridad, ILlavesRepositorio llave)
        {
            this._seguridad = seguridad;
            this._usuarioRepositorio = usuarioRepositorio;
            _llave = llave;
        }


        [HttpGet("ObtenerUsuarioPorId/{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound("No existe el usuario");
            }
            return Ok(usuario);
        }

        [Authorize]
        [HttpGet("obtenerUsuarios")]
        public async Task<IActionResult> OtenerUsuarios()
        {
            var listUsuario = await _usuarioRepositorio.ObtenerUsuario();
            if (listUsuario == null)
            {
                return NotFound("no existe el usuario");
            }
            return Ok(listUsuario);
        }

        [HttpPost("Login")]
        public  async Task <IActionResult> Login([FromBody] UserLogin user)
        {
            if (user == null)
                return BadRequest("Error datos no validos");
            var token = await _seguridad.Autenticacion(user);
            if (token == null)
            {
                return BadRequest("error en el usuario o contrase�a ");
            }
            
            return Ok(token);
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody]SeguridadDTO seguridad)
        {

            if(seguridad==null)
            {
                return BadRequest("Error en el payload");
            }

            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorId(seguridad.IdUsuario);
            if(usuario == null)
            {
                return BadRequest("No existe el usuario en el sistema");
            }

            var existeUsuario= _seguridad.GetUsuarioSeguridad(seguridad);
            if(existeUsuario==null)
            {
                return BadRequest("Error, usuario ya existe en este servicio");
            }
            var usuarioCreado = await _seguridad.CrearUsuarioSeguridad(seguridad);

            return Ok($"se creo correctamente el usuario {usuarioCreado.IdUsuario}");

            
            //return Ok($"usuario : {usuarioCreado.Usuario} creado correctamente");
        }
    }
}