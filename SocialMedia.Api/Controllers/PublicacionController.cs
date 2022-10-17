using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMedia.Core.Entidades;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Repositorios;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    public class PublicacionController : Controller
    {
        
        private readonly IPublicacionRepositorio _publicacionRepositorio;

        public PublicacionController(IPublicacionRepositorio publicacionRepositorio)
        {
            _publicacionRepositorio = publicacionRepositorio;
        }


        [HttpGet("ObtenerPublicaciones")]
        public async Task<IActionResult> ObtenerPublicaciones()
        {
            var publicaciones= await _publicacionRepositorio.GetPublicaciones();
            
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
       
    }
}