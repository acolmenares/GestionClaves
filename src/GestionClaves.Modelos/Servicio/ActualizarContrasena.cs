using GestionClaves.Modelos.Config;
using GestionClaves.Modelos.Interfaces;
using ServiceStack;

namespace GestionClaves.Modelos.Servicio
{
    public class ActualizarContrasena:IReturn<ActualizarContrasenaResponse>, IHasCaptcha
    {
        public string Usuario { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
        public string Captcha { get; set; }
    }

    public class ActualizarContrasenaResponse : IHasResponseStatus
    {
        public MailResponse CorreoResponse { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
