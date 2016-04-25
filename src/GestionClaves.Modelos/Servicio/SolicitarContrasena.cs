using GestionClaves.Modelos.Config;
using GestionClaves.Modelos.Interfaces;
using ServiceStack;

namespace GestionClaves.Modelos.Servicio
{
    public class SolicitarContrasena:IReturn<SolicitarContrasenaResponse>, IHasCaptcha
    {
        public string Correo { get; set; }
        public string Captcha { get; set; }
    }

    public class SolicitarContrasenaResponse :IHasResponseStatus
    {
        public MailResponse MailResponse { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
