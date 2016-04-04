using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Servicio
{
    public class ActualizarClave
    {
        public string Login { get; set; }
        public string AntiguaContrasena { get; set; }
        public string NuevaContrasena { get; set; }
    }

    public class ActualizarClaveResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus
        {
            get; set;
        }
    }
}
