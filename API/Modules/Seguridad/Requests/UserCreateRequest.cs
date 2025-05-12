namespace Ferreteria.GestionVentas.API.Modules.Seguridad.Requests
{
    public class UserCreateRequest
    {
        public string Sociedad { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
        public string ConfirmarContrasenia { get; set; }
    }
}
