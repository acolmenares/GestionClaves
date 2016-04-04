using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IValidador<T>
    {
        void ValidateAndThrow(T instance);
    }
}
