using System;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;
using ServiceStack.Configuration;
using ServiceStack.FluentValidation.Results;
using ServiceStack.FluentValidation;

namespace GestionClaves.BL.Gestores

{
    public class GestorUsuarios : IGestorUsuarios
    {
        public IAlmacenUsuarios AlmacenUsuarios { get; set; }
        public IValidadorGestorUsuarios ValidadorGestorUsuarios { get; set; }
        public IProveedorHash ProveedorHash { get; set; }
        public ICorreo Correo { get; set; }

        public GestorUsuarios()
        {
            var appSettings = new AppSettings();
        }
        
        public ActualizarClaveResponse ActualizarContrasena(ActualizarClave request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = AlmacenUsuarios.ConsultarPorLogin(request.Login);
            ValidadorGestorUsuarios.ValidarLoginContrasena(usuario);
            VerificarContraseña(request, usuario);
            AsignarContrasena(usuario, request.NuevaContrasena);
            AlmacenUsuarios.ActualizarClave(usuario);
            var cr = Correo.EnviarNotificacionActualizacionContrasena(usuario);
            return new ActualizarClaveResponse { CorreoResponse = cr };
        }
                

        public GenerarContrasenaResponse GenerarContrasena(GenerarContrasena request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = AlmacenUsuarios.ConsultarPorLogin(request.Login);
            ValidadorGestorUsuarios.ValidarLogin(usuario);
            var nuevaContrasena = CreateRandomPassword();
            AsignarContrasena(usuario, nuevaContrasena);
            AlmacenUsuarios.ActualizarClave(usuario);
            var cr = Correo.EnviarNotificacionGeneracionContrasena(usuario, nuevaContrasena);
            return new GenerarContrasenaResponse { CorreoResponse = cr };
        }

        private void VerificarContraseña(ActualizarClave request,Usuario usuario)
        {
            if (!ProveedorHash.VerificarHash(request.AntiguaContrasena, usuario.Contrasena))
            {
                var result = new ValidationResult(new[] { new ValidationFailure("Usuario", "Usuario / Contraseña inválidos", "") });
                throw new ValidationException(result.Errors);
            }
        }


        private void AsignarContrasena(Usuario usuario, string nuevaContrasena)
        {
            string hash;
            string salt;
            ProveedorHash.ObtenerHash(nuevaContrasena, out hash, out salt);
            usuario.Contrasena = hash;
        }

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
    }
}
