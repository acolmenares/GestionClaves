using GestionClaves.Modelos.Config;
using GestionClaves.Modelos.Entidades;

namespace GestionClaves.Modelos.Interfaces
{
    public interface ICorreo
    {
        MailResponse EnviarNotificacionActualizacionContrasena(Usuario usuario);
        MailResponse EnviarNotificacionGeneracionContrasena(Usuario usuario, string nuevaContrasena);
        MailResponse EnviarTokenGeneracionContrasena(Usuario usuario);
    }

    
}