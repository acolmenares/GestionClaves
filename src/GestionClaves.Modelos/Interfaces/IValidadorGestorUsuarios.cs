using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IValidadorGestorUsuarios
    {
        IValidador<ActualizarClave> ValidadorActualizarClave { get; set; }
        IValidador<Usuario> ValidadorLoginContrasena { get; set; }
        
        void ValidarPeticion(ActualizarClave request);
        void ValidarPeticion(GenerarContrasena request);

        void ValidarLoginContrasena(Usuario usuario);
        void ValidarLogin(Usuario usuario);


    }
}
