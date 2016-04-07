using GestionClaves.Modelos.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IGestorUsuarios
    {
        ActualizarClaveResponse ActualizarContrasena(ActualizarClave request);
        GenerarContrasenaResponse GenerarContrasena(GenerarContrasena request);

    }
}
