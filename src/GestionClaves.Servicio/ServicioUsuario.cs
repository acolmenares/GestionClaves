using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;

namespace GestionClaves.Servicio
{
    public class ServicioUsuario : ServicioBase
    {
        public IGestorUsuarios GestorUsuarios { get; set; }

        public ActualizarContrasenaResponse Post(ActualizarContrasena request)
        {
            ValidarCaptcha(request);
            return GestorUsuarios.ActualizarContrasena(request);
        }

        public ConfirmarContrasenaResponse Post(ConfirmarContrasena request)
        {
            ValidarCaptcha(request);
            return GestorUsuarios.GenerarContrasena(request);    
        }

        public SolicitarContrasenaResponse Post(SolicitarContrasena request)
        {
            ValidarCaptcha(request);
            return GestorUsuarios.SolicitarGeneracionContrasena(request);
        }

        private void ValidarCaptcha(IHasCaptcha request)
        {
            var captcha = Captcha;
            Captcha = "";
            ValidateAndThrow(() => !string.IsNullOrEmpty(request.Captcha), "Captcha", "Debe Indicar el texto Captcha", "");
            ValidateAndThrow(() => request.Captcha==captcha, "Captcha", "Texto Captcha no válido", "");
        }

    }
}
