using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.BL.Gestores
{
    public abstract class GestorBase
    {
        protected void ValidateAndThrow(Func<bool> fn, string propertyName = "", string error = "", string errorCode = "")
        {
            if (!fn())
            {
                var result = new ValidationResult(new[] { new ValidationFailure(propertyName, error, errorCode) });
                throw new ValidationException(result.Errors);
            }
        }
    }
}
