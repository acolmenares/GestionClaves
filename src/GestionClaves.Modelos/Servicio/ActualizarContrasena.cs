using GestionClaves.Modelos.Config;
using ServiceStack;

namespace GestionClaves.Modelos.Servicio
{
    public class ActualizarContrasena:IReturn<ActualizarContrasenaResponse>
    {
        public string Usuario { get; set; }
        public string ContrasenaActual { get; set; }
        public string NuevaContrasena { get; set; }
    }

    public class ActualizarContrasenaResponse : IHasResponseStatus
    {
        public MailResponse CorreoResponse { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
