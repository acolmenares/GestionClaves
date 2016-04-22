using GestionClaves.Modelos.Interfaces;
using GestionClaves.Modelos.Servicio;

namespace GestionClaves.Servicio
{
    public class ServicioCaptcha:ServicioBase
    {
        public IProveedorValores Valores { get; set; }

        public GenerarCaptchaResponse Get(GenerarCaptcha request)
        {
            Captcha = Valores.CrearTextoCaptcha();
            var sig64 = Valores.GenerarBase64Captcha(Captcha);
            return new GenerarCaptchaResponse { TextoBase64 = sig64 };    
        }

        public GenerarCaptchaResponse Post(GenerarCaptcha request)
        {
            return Get(request);
        }
    }
}
