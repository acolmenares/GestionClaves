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
        ActualizarContrasenaResponse ActualizarContrasena(ActualizarContrasena request);
        ConfirmarContrasenaResponse GenerarContrasena(ConfirmarContrasena request);
        SolicitarContrasenaResponse SolicitarGeneracionContrasena(SolicitarContrasena request);
    }
}
