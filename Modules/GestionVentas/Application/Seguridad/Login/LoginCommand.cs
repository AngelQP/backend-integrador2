using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Modules.GestionVentas.Application.Contract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Seguridad.Login
{
    public class LoginCommand : CommandBase<RequestResult>
    {
        public LoginCommand(string usuario, string contrasenia, bool recordarme, string returnUrl, IConfiguration configuration)
        {
            Usuario = usuario;
            Contrasenia = contrasenia;
            Recordarme = recordarme;
            ReturnUrl = returnUrl;
            Configuration = configuration;
        }

        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public bool Recordarme { get; set; }
        public string ReturnUrl { get; set; }
        public IConfiguration Configuration { get; set; }
    }
}
