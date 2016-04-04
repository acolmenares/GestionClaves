using System;
using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;
using ServiceStack.Configuration;

namespace GestionClaves.BL.Gestores

{
    public class GestorUsuarios:IGestorUsuarios
    {
        public IAlmacenUsuarios AlmacenUsuarios { get; set; }
        public IValidadorGestorUsuarios ValidadorGestorUsuarios { get; set; }

        public GestorUsuarios()
        {
            var appSettings = new AppSettings();
        }
        
        public void ActualizarContrasena(ActualizarClave request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = AlmacenUsuarios.ConsultarPorLoginClave(request.Login, request.AntiguaContrasena);
            ValidadorGestorUsuarios.ValidarLoginContrasena(usuario);
            usuario.Contrasena = request.NuevaContrasena;
            AlmacenUsuarios.ActualizarClave(usuario);
        }

        public void GenerarContrasena(GenerarContrasena request)
        {
            ValidadorGestorUsuarios.ValidarPeticion(request);
            var usuario = AlmacenUsuarios.ConsultarPorLogin(request.Login);
            ValidadorGestorUsuarios.ValidarLogin(usuario);
            AlmacenUsuarios.GenerarClave(usuario);

        }
    }
}
