using Bigstick.BuildingBlocks.Application.Response;
using Ferreteria.Comunications.Application.Core.Mail.DTO;
using Ferreteria.Comunications.Application.Core.Mail;
using Ferreteria.Modules.GestionVentas.Application.Configuration.Command;
using Ferreteria.Modules.GestionVentas.Application.Seguridad.ForgotPassword;
using Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad;
using Ferreteria.Modules.GestionVentas.Domain.Plantillas;
using Ferreteria.Modules.GestionVentas.Domain.Repository;
using Ferreteria.Modules.GestionVentas.Domain.Servicios;
using Ferreteria.Modules.GestionVentas.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Application.Producto.Notification
{
    public class NotificationCommandHandler : ICommandHandler<NotificationCommand, RequestResult>
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly ICommonService _commonService;
        private readonly IMailService _mailService;
        private readonly IUserContext _userContext;

        public NotificationCommandHandler(ISeguridadRepository seguridadRepository, IProductoRepository productoRepository, ICommonService commonService, IMailService mailService, IUserContext userContext)
        {
            _productoRepository = productoRepository;
            _seguridadRepository = seguridadRepository;
            _commonService = commonService;
            _mailService = mailService;
            _userContext = userContext;
        }

        public async Task<RequestResult> Handle(NotificationCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.ProductGetById(request.IdProducto);
            if (producto == null)
            {
                return RequestResult.WithError("Producto no encontrado.");
            }

            var htmlBody = @"<!DOCTYPE html>
                            <html>
                            <head>
                                <style>
                                    body {
                                        font-family: Arial, sans-serif;
                                        text-align: center;
                                    }
                                    .containerMain {
                                        width: 700px;
                                        margin: 0 auto;
                                        background-color: #FFFFFF;
                                    }
                                    .header {
                                        padding-right: 2px;
                                        color: #8e918e;
                                        text-align: right;
                                        background-color: #FFFFFF;
                                    }
                                    .container {
                                        width: 700px;
                                        margin: 0 auto;
                                        background-color: #F4EBE0;
                                        padding: 20px;
                                        border-radius: 20px;
                                        text-align: center;
                                    }
                                    .title {
                                        color: #0a6d54;
                                        font-size: 36px;
                                        font-weight: bold;
                                        margin: 20px 0;
                                    }
                                    .subtitle {
                                        font-size: 18px;
                                        margin: 10px 0;
                                    }
                                    .factura {
                                        color: #0a6d54;
                                        font-weight: bold;
                                    }
                                    .company-box {
                                        background-color: #F5C400;
                                        padding: 15px;
                                        border-radius: 5px;
                                        display: inline-block;
                                        margin: 10px 0;
                                        font-weight: bold;
                                    }
                                    .fecha {
                                        color: #0a6d54;
                                        margin: 15px 0;
                                    }
                                </style>
                            </head>
                            <body>
                                <div class='containerMain'>
                                    <div class='header'>Vende y gestiona tu negocio</div>
                                    <div class='container'>
                                        <div class='title'>¡Ferreteria Web!</div>
                                        <div class='subtitle'>Producto <span class='factura'>{{producto.Nombre}}</span> se esta quedando sin Stock</div>
                                        <div class='fecha'>Cantidad actual {{producto.Stock}} unidades</div>
                                        <div class='company-box'>
                                            <strong>Ir a la Web</strong>
                                        </div>
                                    </div>
                                </div>
                            </body>
                            </html>";

            // Reemplazo de placeholders
            htmlBody = htmlBody
                .Replace("{{producto.Nombre}}", producto.Nombre)
                .Replace("{{producto.Stock}}", producto.Stock.ToString());

            var emailRequest = new EmailRequest
            {
                TO = new List<string> { request.Correo },
                Subject = "Ferretería - Notificación simple",
                Body = htmlBody,
                IsBodyHtml = true
            };

            return await _mailService.Send(emailRequest);
        }
    }
}