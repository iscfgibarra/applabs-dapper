using System;
using System.Data;

namespace AppLabs.Dapper.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        
        IDbTransaction Transaction { get; set; }
        IDbConnection  Connection { get; set; }
        void SaveChanges();
    }
}
