using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IProveedorValores
    {
        string CrearTextoCaptcha();
        string CrearContrasenaAleatoria();
        string GenerarBase64Captcha(string texto);
    }
}
