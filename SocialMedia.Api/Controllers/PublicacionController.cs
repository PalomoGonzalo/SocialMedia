using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Repositorios;
using SocialMedia.Infraestructura.Filtro;

namespace SocialMedia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacionController : ControllerBase
    {
        
        private readonly IPublicacionRepositorio _publicacionRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public PublicacionController(IPublicacionRepositorio publicacionRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _publicacionRepositorio = publicacionRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }


        [HttpGet("ObtenerPublicaciones")]
        public async Task<IActionResult> ObtenerPublicaciones()
        {
            var publicaciones= await _publicacionRepositorio.GetPublicaciones();

            if(publicaciones==null)
            {
                return NotFound("No existe publicaciones");  
            }
            
            return Ok(publicaciones);
        }

        [HttpGet("ObtenerPublicacionPorId/{id}")]
        public async Task<IActionResult> ObtenerPublicacionPorId(int id)
        {
            var publicacionPorId= await _publicacionRepositorio.GetPublicacion(id);
            if(publicacionPorId==null)
            {
                return NotFound($"El id {id} no existe");
            }

            return Ok(publicacionPorId);
        }

        [HttpPost("CrearPublicacion")]
        public async Task<IActionResult> CrearPublicacion ([FromBody] PublicacionCreacionDTO publicacionCreacionDTO)
        {
            if (publicacionCreacionDTO == null)
                return BadRequest("Error en el payload");

            if(await _usuarioRepositorio.ExisteUsuarioPorId(publicacionCreacionDTO.IdUsuario)==null)
            {
                return NotFound("no existe el id usuario");
            }
            
            if(await _publicacionRepositorio.CrearPublicacion(publicacionCreacionDTO)>0)
            {
                return Ok(publicacionCreacionDTO);
            }

            return BadRequest();

        }
       
    }
}