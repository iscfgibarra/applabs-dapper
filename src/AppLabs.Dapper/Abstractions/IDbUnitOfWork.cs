using System;
using System.Data;

namespace AppLabs.Dapper.Abstractions;

public interface IDbUnitOfWork : IDisposable
{
    IDbTransaction Transaction { get; set; }
    IDbConnection Connection { get; set; }
    IDbProvider Provider { get; set; }
    void SaveChanges();
}