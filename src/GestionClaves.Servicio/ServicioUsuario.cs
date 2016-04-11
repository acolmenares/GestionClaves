using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Servicio
{
    public class ServicioUsuario : ServicioBase
    {
        public IGestorUsuarios GestorUsuarios { get; set; }

        public ActualizarContrasenaResponse Post(ActualizarContrasena request)
        {
            return GestorUsuarios.ActualizarContrasena(request);
        }

        public GenerarContrasenaResponse Post(GenerarContrasena request)
        {
            return GestorUsuarios.GenerarContrasena(request);    
        }

        public SolicitarGeneracionContrasenaResponse Post(SolicitarGeneracionContrasena request)
        {
            ValidarCaptcha(request);
            Captcha = "";
            return GestorUsuarios.SolicitarGeneracionContrasena(request);
        }

        private void ValidarCaptcha(SolicitarGeneracionContrasena request)
        {
            ValidateAndThrow(() => !string.IsNullOrEmpty(request.Captcha), "Captcha", "Debe Indicar el texto Captcha", "");
            ValidateAndThrow(() => request.Captcha==Captcha, "Captcha", "Texto Captcha no válido", "");
        }

    }
}
