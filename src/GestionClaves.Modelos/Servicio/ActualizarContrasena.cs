using GestionClaves.Modelos.Config;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
