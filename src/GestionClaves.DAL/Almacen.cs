using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace GestionClaves.DAL
{
    public class Almacen<T>where T:class
    {
        protected IDbConnectionFactory DbConnectionFactory;

        public Almacen(IDbConnectionFactory dbConnectionFactory) {
            DbConnectionFactory = dbConnectionFactory;
        }

        public T ConsultarSimple(Expression<Func<T,bool>> predicate)
        {
            using ( var con = DbConnectionFactory.Open())
            {
                return con.Single<T>(predicate);
            }
        }

        public List<T> Consultar(Expression<Func<T, bool>> predicate)
        {
            using (var con = DbConnectionFactory.Open())
            {
                return con.Select<T>(predicate);
            }
        }

        public int Actualizar(T data, SqlExpression<T> updateOnly=null)
        {
            int c = 0;
            using (var con = DbConnectionFactory.Open())
            {
                if(updateOnly != null)
                {
                    c= con.UpdateOnly<T>(data, updateOnly);
                }
                else
                {
                   c= con.Update<T>(data);
                }
                
            }
            return c;
        }

    }
}
