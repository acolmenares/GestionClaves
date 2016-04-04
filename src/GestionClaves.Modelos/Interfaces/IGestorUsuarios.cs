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
        IAlmacenUsuarios AlmacenUsuarios { get; set; }
        IValidadorGestorUsuarios ValidadorGestorUsuarios { get; set; }
        void ActualizarContrasena(ActualizarClave request);
        void GenerarContrasena(GenerarContrasena request);
    }
}
