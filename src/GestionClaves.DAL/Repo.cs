using GestionClaves.Modelos.Interfaces;
using System;
using System.Collections.Generic;

namespace GestionClaves.DAL
{
    public class Repo<T> : IRepo<T> where T : IEntidad
    {
        
        public int Actualizar(IConexion conexion, T data)
        {
            return conexion.Actualizar(data);
        }

        public int Actualizar<TKey>(IConexion conexion, T data, System.Linq.Expressions.Expression<Func<T, bool>> predicate, System.Linq.Expressions.Expression<Func<T, TKey>> onlyFields)
        {
            return conexion.Actualizar<T, TKey>(data, predicate, onlyFields);
        }

        public int Actualizar(IConexion conexion, T data, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return conexion.Actualizar(data, predicate);
        }

        public int Borrar(IConexion conexion, int id)
        {
            return conexion.Borrar<T>(id);
        }

        public List<T> Consultar(IConexion conexion, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return conexion.Consultar(predicate);
        }

        public T ConsultarPorId(IConexion conexion, int id)
        {
            return conexion.ConsultarPorId<T>(id);
        }

        public T ConsultarSimple(IConexion conexion, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return conexion.ConsultarSimple(predicate);
        }

        public void Crear(IConexion conexion, T data)
        {
            conexion.Crear(data);
        }

        
    }
}
