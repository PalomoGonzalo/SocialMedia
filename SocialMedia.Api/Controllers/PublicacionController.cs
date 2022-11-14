using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Core.DTOS;
using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using static SocialMedia.Core.Enumeraciones.EnumsLib;

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
        public async Task<IActionResult> ObtenerPublicaciones(int pagina, int cantidadRegistros)
        {
            var publicaciones= await _publicacionRepositorio.GetPublicaciones(pagina, cantidadRegistros);

            if(publicaciones==null)
            {
                return NotFound("No existe publicaciones");  
            }
            var cantidadDePaginasTotales= await _publicacionRepositorio.ObtenerCantidadDePaginas(cantidadRegistros);

            Response.Headers.Add("CantidadDePaginas", JsonConvert.SerializeObject(cantidadDePaginasTotales));
            
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

            if(await _usuarioRepositorio.ObtenerUsuarioPorId(publicacionCreacionDTO.IdUsuario)==null)
            {
                return BadRequest("no existe el id usuario");
            }

            if(await _publicacionRepositorio.ObtenerCantidadDepubicacionesPorUsuarioID(publicacionCreacionDTO.IdUsuario)>= (int)CantidadPublicaciones.Max)
            {
                return BadRequest("Error, numeros de publicaciones superados");
            }
            
            if(await _publicacionRepositorio.CrearPublicacion(publicacionCreacionDTO)>0)
            {
                return Ok(publicacionCreacionDTO);
            }
            
            return BadRequest("Error al crear la publicacion");
        }


        [HttpPut("ModificarDescripcionPublicacion/{id}")]
        public async Task<IActionResult> ModificarDescripcipDePublicacion([FromBody] string descripcion,int id)
        {
            var publicacion = await _publicacionRepositorio.GetPublicacion(id);
            if (publicacion == null)
            {
                return NotFound("no existe la publicacion");
            }
            if(await _publicacionRepositorio.ModificarDescripcionPublicacion(descripcion,id)>0)
            {
                return Ok($"Se modifico correctamente el comentario");
            }
            return BadRequest("No se logro modificar la descripcion");
            
        }

        [HttpGet("ObtenerCantidadDepubicacionPorUsuario")]
        public async Task<IActionResult> ObtenerCantidadPublicacionPorUsuario()
        {
            var lista = await _publicacionRepositorio.ObtenerCantidadDepubicacionesPorUsuario();
            return Ok(lista);
        }

        [HttpGet("ObtenerCantidadDepubicacionPorUsuarioId/{id}")]
        public async Task<IActionResult> ObtenerCantidadDepubicacionPorUsuarioId(int id)
        {
            var cantidad = await _publicacionRepositorio.ObtenerCantidadDepubicacionesPorUsuarioID(id);
            return Ok(cantidad);
        }

        [HttpGet("Test")]

        public IActionResult Test()
        {
            Usuario user = new Usuario();
            user.Apellidos = "matias";
            user.Nombres = "Gonzalo";
            var test = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return Ok($"{user.Nombres} and {user.Apellidos}, usando json {test}");


        }




    }
}