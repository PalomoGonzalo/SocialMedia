namespace SocialMedia.Infraestructura.Options
{
    public class PasswordOptions
    {
        public int SaltSize { get; set; }

        public int KeySize { get; set; }
        public int Iteraciones { get; set; }
    }
}