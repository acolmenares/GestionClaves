using GestionClaves.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Model;
using System.Linq.Expressions;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace GestionClaves.DAL
{
    public class ConexionBD : IConexion
    {
        protected  IDbConnection conexion;
        protected  IDbTransaction transaccion=null;
        
        public ConexionBD(IDbConnectionFactory dbConnectionFactory, bool crearTransaccion =false)
        {
            conexion = dbConnectionFactory.Open();
            if (crearTransaccion) Execute(con => transaccion = con.OpenTransaction());
            Console.WriteLine("conexionDB conexion {0}", conexion);

        }


        public void IniciarTransaccion()
        {
            if (transaccion != null)
            {
                Execute(con => transaccion = con.OpenTransaction());
            }
        }

        public void AceptarCambios()
        {
            if (transaccion != null)
            {
                transaccion.Commit();
                transaccion.Dispose();
                transaccion = null;
            }
        }

        public int Actualizar<T>(T data) where T : IHasIntId
        {
            int r = 0;
            Execute(con =>{
                r = con.Update<T>(data, f => f.Id == data.Id);
            });
            return r;
        }

        public int Actualizar<T, TKey>(T data, Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> onlyFields)
        {
            int c = 0;
            Execute(con => {
                var updateOnly = con.From<T>().Where(predicate).Update(onlyFields);
                c = con.UpdateOnly<T>(data, updateOnly);
            });

            return c;
        }

        public int Actualizar<T, TKey>(T data, Expression<Func<T, bool>> predicate)
        {
            int c = 0;
            Execute(con => {
                var updateOnly = con.From<T>().Where(predicate);
                c = con.UpdateOnly<T>(data, updateOnly);
            });

            return c;
        }


        public int Borrar<T>(int id) where T : IHasIntId
        {
            throw new NotImplementedException();
        }

        public List<T> Consultar<T>(Expression<Func<T, bool>> predicate)
        {
            return  Execute<List<T>> (con => con.Select(predicate));
            
        }

        public T ConsultarPorId<T>(int id) where T : IHasIntId
        {
            return   Execute<T>(con =>  con.SingleById<T>(id));
        }

        public T ConsultarSimple<T>(Expression<Func<T, bool>> predicate)
        {
            return   Execute<T>(con => con.Single(predicate));
        }

        public void Crear<T>(T data) where T : IEntidad
        {
            Execute(con => {
                con.Insert(data);
                data.Id = int.Parse(con.LastInsertId().ToString());
            });
        }

        public void DeshacerCambios()
        {
            Rollback();
        }

        

        public void Dispose()
        {
            Rollback();
            Execute(con => con.Dispose());
        }


        private void Rollback()
        {
            if (transaccion != null)
            {
                transaccion.Rollback();
                transaccion.Dispose();
                transaccion = null;
            }
        }

        protected virtual void Execute(Action<IDbConnection> acciones)
        {
            acciones(conexion);
        }

        protected virtual T Execute<T>(Func<IDbConnection,T> acciones)
        {
            return acciones(conexion);
        }

        
    }
}
