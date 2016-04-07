using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Interfaces;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.DAL
{
    public class RepoUsuario : Repo<Usuario>, IRepoUsuario
    {
        public int ActualizarClave(IConexion conexion, Usuario usuario)
        {
            return  Actualizar( conexion,usuario, q => q.Contrasena, q => q.Id == usuario.Id);
        }
               

        public Usuario ConsultarPorLogin(IConexion conexion, string login)
        {
            return ConsultarUsuario(conexion, q => q.Contrasena == login);
        }


        private Usuario ConsultarUsuario(IConexion conexion, Expression<Func<Usuario, bool>> predicate)
        {
            return NormalizarUsuario(ConsultarSimple(conexion, predicate));
        }


        private static Usuario NormalizarUsuario(Usuario usuario)
        {
            if (usuario != default(Usuario))
            {
                if (!string.IsNullOrEmpty(usuario.Correo)) usuario.Correo = usuario.Correo.Trim();
                if (!string.IsNullOrEmpty(usuario.Login)) usuario.Login = usuario.Login.Trim();
                if (!string.IsNullOrEmpty(usuario.NombreCompleto)) usuario.NombreCompleto = usuario.NombreCompleto.Trim();

            }
            return usuario;
        }

    }
}
