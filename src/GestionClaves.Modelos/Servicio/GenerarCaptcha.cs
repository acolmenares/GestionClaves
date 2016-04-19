using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Servicio
{
    public class GenerarCaptcha : IReturn<GenerarCaptchaResponse>
    { }

    public class GenerarCaptchaResponse:IHasResponseStatus
    {
        public ResponseStatus ResponseStatus
        {
            get; set;
        }

        public string TextoBase64 { get; set; }
    }

}
