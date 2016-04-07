using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IRepo<T> where T:  IEntidad
    {
        List<T> Consultar(IConexion conexion, Expression<Func<T, bool>> predicate);
        T ConsultarSimple(IConexion conexion, Expression<Func<T, bool>> predicate);
        T ConsultarPorId(IConexion conexion, int id);
        int Actualizar<TKey>(IConexion conexion, T data, Expression<Func<T, TKey>> onlyFields, Expression<Func<T, bool>> predicate);
        int Actualizar(IConexion conexion, T data) ;
        void Crear(IConexion conexion, T data) ;
        int Borrar(IConexion conexion, int id);
        
    }
}
