using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionClaves.Modelos.Interfaces
{
    public interface IConexion:IDisposable
    {
        List<T> Consultar<T>(Expression<Func<T, bool>> predicate);
        T ConsultarSimple<T>(Expression<Func<T, bool>> predicate);
        T ConsultarPorId<T>(int id) where T : IHasIntId;
        int Actualizar<T, TKey>(T data, Expression<Func<T, TKey>> onlyFields, Expression<Func<T, bool>> predicate);
        int Actualizar<T>(T data) where T : IHasIntId;
        void Crear<T>(T data) where T : IEntidad;
        int Borrar<T>(int id) where T : IHasIntId;
        void AceptarCambios();
        void DeshacerCambios();

    }
}
