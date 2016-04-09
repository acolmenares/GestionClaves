using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IFabricaConexiones
    {
        IConexion Crear(bool crearTransaccion =false);
        void Ejecutar(Action<IConexion> conexion, bool crearTransaccion = false);
        T Ejecutar<T>(Func<IConexion, T> conexion, bool crearTransaccion = false) where T : IEntidad;
    }
}
