using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Servicio
{
    public class ServicioUsuario : ServicioBase
    {
        public IGestorUsuarios GestorUsuarios { get; set; }

        public ActualizarClaveResponse Post(ActualizarClave request)
        {
            GestorUsuarios.ActualizarContrasena(request);
            return new ActualizarClaveResponse();
        }

        public GenerarContrasenaResponse Post(GenerarContrasena request)
        {
            GestorUsuarios.GenerarContrasena(request);
            return new GenerarContrasenaResponse();
        }
    }
}
