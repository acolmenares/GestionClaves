using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IProveedorHash
    {
        void ObtenerHash(string data, out string hash, out string salt);
        bool VerificarHash(string data, string hash, string salt="");
    }
}
