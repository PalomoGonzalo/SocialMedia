using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("[controller]")]
    public class LLaveApiController : Controller
    {
        private readonly ILlavesRepositorio _llave;
        

        public LLaveApiController(ILlavesRepositorio llave)
        {
            _llave = llave;
        }
    }
}