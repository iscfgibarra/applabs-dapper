using System.Data;
using AppLabs.QueryExpression;

namespace AppLabs.Dapper.Abstractions;

public interface IDbProvider
{
    string ProviderName { get; set; }
    DbProviderType ProviderType { get; set; }
    string GetConnectionString();
    IDbConnection GetConnection();
    DbTypes GetDbType { get; }
}