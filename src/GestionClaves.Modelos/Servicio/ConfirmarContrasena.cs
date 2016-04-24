using GestionClaves.Modelos.Config;
using GestionClaves.Modelos.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Servicio
{
    public class ConfirmarContrasena:IReturn<ConfirmarContrasenaResponse>, IHasCaptcha
    {
        public string Usuario { get; set; }
        public string Token { get; set; }
        public string Captcha { get; set; }
    }


    public class ConfirmarContrasenaResponse : IHasResponseStatus
    {
        public MailResponse CorreoResponse { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
