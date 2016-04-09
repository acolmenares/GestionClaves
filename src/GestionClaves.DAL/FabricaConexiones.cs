using GestionClaves.Modelos.Interfaces;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.DAL
{
    public class FabricaConexiones : IFabricaConexiones
    {
        IDbConnectionFactory dbConnectionFactory;
        public FabricaConexiones(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public IConexion Crear(bool crearTransaccion = false)
        {
            return new ConexionBD(dbConnectionFactory, crearTransaccion);
        }

        public void Ejecutar(Action<IConexion> conexion, bool crearTransaccion = false)
        {
            using(var cn = new ConexionBD(dbConnectionFactory, crearTransaccion))
            {
                conexion(cn);
            }
        }

        public T Ejecutar<T>(Func<IConexion,T> conexion, bool crearTransaccion = false) where T :IEntidad
        {
            var r = default(T);
            using (var cn = new ConexionBD(dbConnectionFactory, crearTransaccion))
            {
                r = conexion(cn);
            }
            return r;
        }
    }
}
