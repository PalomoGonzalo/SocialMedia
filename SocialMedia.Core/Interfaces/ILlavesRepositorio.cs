using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.DTOS;
using static SocialMedia.Core.Enumeraciones.EnumsLib;

namespace SocialMedia.Core.Interfaces
{
    public interface ILlavesRepositorio
    {
        Task CrearLLave(string usuario,TipoLlave tipoLLave);

       
    }
}