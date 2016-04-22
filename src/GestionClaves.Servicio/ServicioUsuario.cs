using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;

namespace GestionClaves.Servicio
{
    public class ServicioUsuario : ServicioBase
    {
        public IGestorUsuarios GestorUsuarios { get; set; }

        public ActualizarContrasenaResponse Post(ActualizarContrasena request)
        {
            return GestorUsuarios.ActualizarContrasena(request);
        }

        public ConfirmarContrasenaResponse Post(ConfirmarContrasena request)
        {
            return GestorUsuarios.GenerarContrasena(request);    
        }

        public SolicitarContrasenaResponse Post(SolicitarContrasena request)
        {
            ValidarCaptcha(request);
            Captcha = "";
            return GestorUsuarios.SolicitarGeneracionContrasena(request);
        }

        private void ValidarCaptcha(SolicitarContrasena request)
        {
            ValidateAndThrow(() => !string.IsNullOrEmpty(request.Captcha), "Captcha", "Debe Indicar el texto Captcha", "");
            ValidateAndThrow(() => request.Captcha==Captcha, "Captcha", "Texto Captcha no válido", "");
        }

    }
}
