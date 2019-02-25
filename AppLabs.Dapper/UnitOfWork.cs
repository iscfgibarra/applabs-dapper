using System;
using System.Data;
using AppLabs.Dapper.Interfaces;

namespace AppLabs.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnection connection)
        {
            Connection = connection;
            Transaction = Connection.BeginTransaction();
        }
        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }

            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }

            GC.SuppressFinalize(this);
        }

       
        public IDbTransaction Transaction { get; set; }
        public IDbConnection Connection { get; set; }
        public void SaveChanges()
        {
            if (Transaction == null)
                throw new InvalidOperationException("La transaccion es nula verifica el handler de la conexión.");

            Transaction.Commit();
            Transaction = null;
        }
    }
}
