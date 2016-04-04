using GestionClaves.Modelos.Config;
using GestionClaves.Modelos.Entidades;

namespace GestionClaves.Modelos.Interfaces
{
    public interface ICorreo
    {
        CorreoResponse EnviarNotificacionActualizacionContrasena(Usuario usuario);
        CorreoResponse EnviarNotificacionGeneracionContrasena(Usuario usuario, string nuevaContrasena);
    }

    
}