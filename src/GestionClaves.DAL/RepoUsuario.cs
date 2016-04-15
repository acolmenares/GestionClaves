using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Interfaces;
using System;
using System.Linq.Expressions;

namespace GestionClaves.DAL
{
    public class RepoUsuario : Repo<Usuario>, IRepoUsuario
    {
        public int ActualizarContrasena(IConexion conexion, Usuario usuario)
        {
            return Actualizar(conexion, usuario, q => q.Id == usuario.Id,   q => new { q.PasswordHash, q.Salt, q.Token });
        }
               

        public Usuario ConsultarPorNombreUsuario(IConexion conexion, string nombreUsuario)
        {
            return ConsultarUsuario(conexion, q => q.UserName == nombreUsuario);
        }


        public int ActualizarToken(IConexion conexion, Usuario usuario)
        {
            return Actualizar(conexion, usuario, q => q.Id == usuario.Id, q =>  q.Token );
        }

        private Usuario ConsultarUsuario(IConexion conexion, Expression<Func<Usuario, bool>> predicate)
        {
            return NormalizarUsuario(ConsultarSimple(conexion, predicate));
        }


        private static Usuario NormalizarUsuario(Usuario usuario)
        {
            if (usuario != default(Usuario))
            {
                if (!string.IsNullOrEmpty(usuario.Email)) usuario.Email = usuario.Email.Trim();
                if (!string.IsNullOrEmpty(usuario.UserName)) usuario.UserName = usuario.UserName.Trim();
                if (!string.IsNullOrEmpty(usuario.NombreCompleto)) usuario.NombreCompleto = usuario.NombreCompleto.Trim();

            }
            return usuario;
        }

        
    }
}
