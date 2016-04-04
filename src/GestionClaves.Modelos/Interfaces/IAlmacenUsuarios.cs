using GestionClaves.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IAlmacenUsuarios
    {
        //int ActualizarClave(ActualizarClave request);
        //Usuario ConsultarPorLoginClave(ActualizarClave request);
        //Usuario ConsultarPorLoginClave(string login, string contrasena);
        Usuario ConsultarPorLogin(string login);
        int ActualizarClave(Usuario usuario);
       // string GenerarClave(Usuario usuario);
    }
}
