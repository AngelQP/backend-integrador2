﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Modules.GestionVentas.Domain.DTO.Seguridad
{
    public class CrearUsuarioRequest
    {
        public string Sociedad {  get; set; }
        public string Usuario {  get; set; }
        public string Correo {  get; set; }
        public string Nombre {  get; set; }
        public string ApellidoPaterno {  get; set; }
        public string ApellidoMaterno {  get; set; }
        public string Telefono {  get; set; }
        public string Contrasenia {  get; set; }
        public string Rol {  get; set; }
        public string UsuarioCreacion {  get; set; }
    }
}
