using GestionClaves.Modelos;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Interfaces;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.DAL
{
    public class AlmacenUsuarios: Almacen<Usuario>, IAlmacenUsuarios
    {
        public AlmacenUsuarios(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        public Usuario ConsultarPorLogin(string login)
        {
            Expression<Func<Usuario, bool>> predicate =
                (q => q.UserName == login );

            return ConsultarUsuario(predicate);
        }
        
                
        public int ActualizarClave(Usuario usuario)
        {
           
            using (var con = DbConnectionFactory.Open())
            {
                var updateOnly = con.From<Usuario>().Where(q => q.Id == usuario.Id).Update(q => q.PasswordHash);
                return Actualizar(usuario, updateOnly);
            }
        }
                

        private Usuario ConsultarUsuario(Expression<Func<Usuario, bool>> predicate)
        {
            return NormalizarUsuario(ConsultarSimple(predicate));
        }
        

        private static Usuario  NormalizarUsuario(Usuario usuario)
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
