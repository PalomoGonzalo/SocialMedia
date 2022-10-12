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


        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var publicaciones= await _publicacionRepositorio.GetPublicaciones();
            
            return Ok(publicaciones);
        }

       
    }
}