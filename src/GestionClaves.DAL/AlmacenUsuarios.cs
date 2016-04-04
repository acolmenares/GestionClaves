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
                (q => q.Login == login );

            return NormalizarUsuario( ConsultarSimple(predicate));
        }


        public Usuario ConsultarPorLoginClave(string login, string contrasena)
        {
            Expression<Func<Usuario, bool>> predicate =
                (q => q.Login == login && q.Contrasena == GenerarSHA1(contrasena));

            return NormalizarUsuario( ConsultarSimple(predicate));
        }

        
        public int ActualizarClave(Usuario usuario)
        {
            Console.WriteLine(usuario.Contrasena);
            using (var con = DbConnectionFactory.Open())
            {
                
                usuario.Contrasena = GenerarSHA1(usuario.Contrasena);
                var updateOnly = con.From<Usuario>().Where(q => q.Id == usuario.Id).Update(q => q.Contrasena);
                return Actualizar(usuario, updateOnly);
            }
        }

        public string GenerarClave(Usuario usuario)
        {
            var password = CreateRandomPassword();
            usuario.Contrasena = password;
            ActualizarClave(usuario);
            return password;
        }

        /*
        public int ActualizarClave(ActualizarClave request)
        {
            using (var con = DbConnectionFactory.Open())
            {
                var usuario = ConsultarPorLoginClave(request);

                if (usuario == default(Usuario)) return -1;
                if (!usuario.Activo.HasValue || !usuario.Activo.Value) return -2;

                usuario.Contrasena = GenerarSHA1(request.NuevaContrasena);
                var updateOnly = con.From<Usuario>().Where(q => q.Id == usuario.Id).Update(q => q.Contrasena);
                return Actualizar(usuario, updateOnly);
            }
        }

         public Usuario ConsultarPorLoginClave(ActualizarClave request)
        {
            Expression<Func<Usuario, bool>> predicate =
                (q => q.Login == request.Login && q.Contrasena == GenerarSHA1( request.AntiguaContrasena));

            return ConsultarSimple(predicate);
        }

        */


        private static string CreateRandomPassword(int passwordLength = 12)
        {
            const string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789!@$?";
            Byte[] randomBytes = new Byte[passwordLength];
            char[] chars = new char[passwordLength];
            int allowedCharCount = allowedChars.Length;

            for (int i = 0; i < passwordLength; i++)
            {
                var randomObj = new Random();
                randomObj.NextBytes(randomBytes);
                chars[i] = allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }


        private static string GenerarSHA1(string contrasena)
        {
            // Crear una clave SHA1 como la generada por 
            // FormsAuthentication.HashPasswordForStoringInConfigFile
            // Adaptada del ejemplo de la ayuda en la descripción de SHA1 (Clase)
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(contrasena);
            byte[] result = null;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);
            //
            // Convertir los valores en hexadecimal
            // cuando tiene una cifra hay que rellenarlo con cero
            // para que siempre ocupen dos dígitos.
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= result.Length - 1; i++)
            {
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString().ToUpper();
        }


        private static Usuario  NormalizarUsuario(Usuario usuario)
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
