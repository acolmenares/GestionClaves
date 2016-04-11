using GestionClaves.Modelos.Config;
using ServiceStack;

namespace GestionClaves.Modelos.Servicio
{
    public class SolicitarGeneracionContrasena:IReturn<SolicitarGeneracionContrasenaResponse>
    {
        public string Usuario { get; set; }
        public string Captcha { get; set; }
    }

    public class SolicitarGeneracionContrasenaResponse :IHasResponseStatus
    {
        public MailResponse MailResponse { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
