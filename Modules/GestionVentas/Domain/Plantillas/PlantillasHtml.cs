using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.Plantillas
{
    public class PlantillasHtml
    {
        public static string ObtenerCorreoOTP()
        {
            return @"<!DOCTYPE html><html lang=""es""><head><meta charset=""UTF-8"" /><meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/><title>Código de Verificación</title><style>body {font-family: Arial, sans-serif;background-color: #f4f6f8;color: #333;padding: 20px;}.container {max-width: 600px;margin: 0 auto;background-color: #ffffff;padding: 30px;border-radius: 12px;box-shadow: 0 4px 8px rgba(0,0,0,0.1);}.otp {font-size: 36px;font-weight: bold;letter-spacing: 4px;margin: 20px 0;color: #007BFF; /* Azul */text-align: center;}.footer {margin-top: 40px;font-size: 13px;color: #777;text-align: center;}.logo {text-align: center;margin-bottom: 20px;}.logo img {max-width: 150px;}</style></head><body><div class=""container""><div class=""logo""><img src=""https://st5.depositphotos.com/71200068/67265/i/450/depositphotos_672652998-stock-photo-spanner-screwdriver-icon-vector-illustration.jpg"" alt=""Logo Ferretería"" /></div><h2>Hola, {{NOMBRE}}</h2><p>Has solicitado restablecer tu contraseña.</p><p>Usa el siguiente código de verificación para continuar con el proceso:</p><div class=""otp"">{{CODIGO}}</div><p style=""text-align: center; margin-top: 20px;""><strong>Fecha de envío:</strong> {{FECHA_HORA}}</p><p>Este código es válido por <strong>10 minutos</strong>. Si tú no realizaste esta solicitud, puedes ignorar este mensaje con tranquilidad.</p><p>Por tu seguridad, <strong>no compartas este código</strong> con nadie.</p><div class=""footer"">&copy; {{ANIO}}. Todos los derechos reservados.</div></div></body></html>";
        }

        public static string ObtenerCorreoRegistroUsuario()
        {
            return @"<!DOCTYPE html><html lang=""es""><head><meta charset=""UTF-8"" /><meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" /><title>Registro Exitoso</title><style>body {font-family: Arial, sans-serif;background-color: #f4f6f8;color: #333;padding: 20px;}.container {max-width: 600px;margin: 0 auto;background-color: #ffffff;padding: 30px;border-radius: 12px;box-shadow: 0 4px 8px rgba(0,0,0,0.1);}.footer {margin-top: 40px;font-size: 13px;color: #777;text-align: center;}.logo {text-align: center;margin-bottom: 20px;}.logo img {max-width: 150px;}.highlight {color: #007BFF;font-weight: bold;}.info-box {background-color: #f0f4ff;border-left: 4px solid #007BFF;padding: 15px;margin: 20px 0;border-radius: 8px;}</style></head><body><div class=""container""><div class=""logo""><img src=""https://st5.depositphotos.com/71200068/67265/i/450/depositphotos_672652998-stock-photo-spanner-screwdriver-icon-vector-illustration.jpg"" alt=""Logo Ferretería"" /></div><h2>¡Bienvenido, {{NOMBRE}}!</h2><p>Tu cuenta ha sido registrada exitosamente en nuestro sistema.</p><div class=""info-box""><p><strong>Correo:</strong> {{CORREO}}</p><p><strong>Rol asignado:</strong> {{ROL}}</p><p><strong>Sociedad:</strong> {{SOCIEDAD}}</p><p><strong>Fecha de registro:</strong> {{FECHA_HORA}}</p></div><p>Puedes iniciar sesión usando tu correo y la contraseña que se ha establecido durante el registro.</p><div style=""text-align: center; margin: 30px 0;""><a href=""http://localhost:4404/login"" target=""_blank""style=""background-color: #007BFF; color: #fff; text-decoration: none; padding: 12px 24px; border-radius: 6px; display: inline-block; font-weight: bold;"">Iniciar Sesión</a></div><p class=""highlight"">Por razones de seguridad, te recomendamos cambiar tu contraseña tras tu primer inicio de sesión.</p><p>Si tú no realizaste este registro o tienes alguna duda, por favor contacta al administrador del sistema.</p><div class=""footer"">&copy; {{ANIO}}. Todos los derechos reservados.</div></div></body></html>";
        }
    }
}
