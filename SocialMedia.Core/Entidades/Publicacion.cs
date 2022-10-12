namespace SocialMedia.Core.Entidades
{
    public class Publicacion
    {
        
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }


    }
}
