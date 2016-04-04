using GestionClaves.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.BL.Utiles
{
    public class ProveedorHash : IProveedorHash
    {
        public void ObtenerHash(string data, out string hash, out string salt)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] bytes = enc.GetBytes(data);
            byte[] result = null;

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(bytes);
            //
            // Convertir los valores en hexadecimal
            // cuando tiene una cifra hay que rellenarlo con cero
            // para que siempre ocupen dos dígitos.
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= result.Length - 1; i++)
            {
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            hash = sb.ToString().ToUpper();
            salt = "";
        }

        public bool VerificarHash(string data, string hash, string salt="")
        {
            
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(hash)) return false;

            string hashVerificar;
            string saltVerificar;
            ObtenerHash(data, out hashVerificar, out saltVerificar);
            return hash == hashVerificar;

        }
    }
}
