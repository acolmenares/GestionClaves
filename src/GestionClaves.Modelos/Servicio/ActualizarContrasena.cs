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
        public string AntiguaContrasena { get; set; }
        public string NuevaContrasena { get; set; }
    }

    public class ActualizarContrasenaResponse : IHasResponseStatus
    {
        public CorreoResponse CorreoResponse { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
