using ServiceStack;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;
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
            get { return SessionBag.Get<string>("captcha");  }
            set { SessionBag.Set<string>("captcha", value); }
        }


        public void ValidateAndThrow(Func<bool> fn, string propertyName = "", string error = "", string errorCode = "")
        {
            if (!fn())
            {
                var result = new ValidationResult(new[] { new ValidationFailure(propertyName, error, errorCode) });
                throw new ValidationException(result.Errors);
            }
        }

    }
}
