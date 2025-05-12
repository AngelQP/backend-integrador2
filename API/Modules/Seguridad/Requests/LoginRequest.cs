namespace Ferreteria.GestionVentas.API.Modules.Seguridad.Requests
{
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public bool Recordarme { get; set; }
        public string ReturnUrl { get; set; }
    }
}
