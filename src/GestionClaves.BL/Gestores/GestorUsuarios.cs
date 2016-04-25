using System;
using GestionClaves.Modelos.Entidades;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;

namespace GestionClaves.BL.Gestores

{
    public class GestorUsuarios : GestorBase, IGestorUsuarios
    {
        public IValidadorGestorUsuarios ValidadorGestorUsuarios { get; set; }
        public IProveedorHash ProveedorHash { get; set; }
        public ICorreo Correo { get; set; }
        public IFabricaConexiones FabricaConexiones { get; set; }
        public IRepoUsuario RepoUsuario { get; set; }
        public IProveedorValores Valores { get; set; }
        public GestorUsuarios()
        {
        }

        public ActualizarContrasenaResponse ActualizarContrasena(ActualizarContrasena request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = FabricaConexiones.Ejecutar<Usuario>(conexion =>
            {
                var u = RepoUsuario.ConsultarPorNombreUsuario(conexion, request.Usuario);
                ValidadorGestorUsuarios.ValidarActivo(u);
                VerificarContrasena(request, u);
                AsignarContrasena(u, request.NuevaContrasena);
                RepoUsuario.ActualizarContrasena(conexion, u);
                return u;
            });

            var cr = Correo.EnviarNotificacionActualizacionContrasena(usuario); //new CorreoResponse(); //  
            return new ActualizarContrasenaResponse { CorreoResponse = cr };
        }

        public SolicitarContrasenaResponse SolicitarGeneracionContrasena(SolicitarContrasena request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = FabricaConexiones.Ejecutar<Usuario>(conexion =>
            {
                var u = RepoUsuario.ConsultarPorCorreo(conexion, request.Correo);
                ValidadorGestorUsuarios.ValidarActivoConCorreo(u);
                u.Token = Valores.CrearToken();
                RepoUsuario.ActualizarToken(conexion, u);
                return u;
            });
            var cr = Correo.EnviarTokenGeneracionContrasena(usuario); //new CorreoResponse(); //
            return new SolicitarContrasenaResponse { MailResponse = cr };
        }


        public ConfirmarContrasenaResponse GenerarContrasena(ConfirmarContrasena request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var nuevaContrasena = string.Empty;
            var usuario = FabricaConexiones.Ejecutar<Usuario>(conexion =>
            {
                var u = RepoUsuario.ConsultarPorCorreo(conexion, request.Correo);
                ValidadorGestorUsuarios.ValidarActivoConCorreo(u);
                VerificarToken(request, u);
                nuevaContrasena = Valores.CrearContrasenaAleatoria();
                AsignarContrasena(u, nuevaContrasena);
                u.Token = "";
                RepoUsuario.ActualizarContrasena(conexion, u);
                return u;
            });
            var cr = Correo.EnviarNotificacionGeneracionContrasena(usuario, nuevaContrasena); //new CorreoResponse(); //
            return new ConfirmarContrasenaResponse { CorreoResponse = cr };
        }

        private void VerificarToken(ConfirmarContrasena request, Usuario u)
        {
            ValidateAndThrow(() => u.Token == request.Token, "Token", "Código de Confirmación No Válido", "");
        }

        private void VerificarContrasena(ActualizarContrasena request,Usuario usuario)
        {
            ValidateAndThrow(() => ProveedorHash.VerificarHash(request.ContrasenaActual, usuario.PasswordHash, usuario.Salt),
                "Usuario", "Usuario/Contraseña inválidos", "");            
        }
                

        private void AsignarContrasena(Usuario usuario, string nuevaContrasena)
        {
            string hash;
            string salt;
            ProveedorHash.ObtenerHash(nuevaContrasena, out hash, out salt);
            usuario.PasswordHash = hash;
            usuario.Salt = salt;
        }

       
    }
}
