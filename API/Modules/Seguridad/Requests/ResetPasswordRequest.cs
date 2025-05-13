namespace Ferreteria.GestionVentas.API.Modules.Seguridad.Requests
{
    public class ResetPasswordRequest
    {
        public string Correo { get; set; }
        public string OTP { get; set; }
        public string NuevaContrasenia { get; set; }
        public string ConfirmarContrasenia { get; set; }
    }
}
