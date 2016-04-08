using GestionClaves.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IRepoUsuario
    {
        Usuario ConsultarPorLogin(IConexion conexion,string login);
        int ActualizarClave(IConexion conexion, Usuario usuario);
    }
}
