using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Servicio
{
    public class ServicioBase: Service
    {
        public string Captcha {
            get { return this.SessionBag.Get<string>("captcha");  }
            set { SessionBag.Set<string>("captcha", value); }
        }
    }
}
