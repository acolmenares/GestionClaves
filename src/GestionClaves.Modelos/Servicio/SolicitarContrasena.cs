using GestionClaves.Modelos.Config;
using ServiceStack;

namespace GestionClaves.Modelos.Servicio
{
    public class SolicitarContrasena:IReturn<SolicitarContrasenaResponse>
    {
        public string Usuario { get; set; }
        public string Captcha { get; set; }
    }

    public class SolicitarContrasenaResponse :IHasResponseStatus
    {
        public MailResponse MailResponse { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
