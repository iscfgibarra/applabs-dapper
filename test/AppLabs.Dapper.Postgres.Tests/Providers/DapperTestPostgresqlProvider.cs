using System.Data;
using AppLabs.Dapper.Abstractions;
using AppLabs.QueryExpression;
using Npgsql;

namespace AppLabs.Dapper.Postgres.Tests.Providers;

public class DapperTestPostgresqlProvider : IDbProvider
{
    private readonly IPosgresqlConfig _posgresqlConfig;

    public DapperTestPostgresqlProvider(IPosgresqlConfig posgresqlConfig)
    {
        ProviderName = "DapperTestPosgresql";
        ProviderType = DbProviderType.Postgres;
        _posgresqlConfig = posgresqlConfig;
    }
    
    public string ProviderName { get; set; }
    public DbProviderType ProviderType { get; set; }
    public string GetConnectionString()
    {
        return _posgresqlConfig.ConnectionString;
    }

    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(GetConnectionString());
    }

    public DbTypes GetDbType => DbTypes.PgSql;
}

public interface IPosgresqlConfig
{
    string ConnectionString { get; set; }
}

class PosgresqlConfig : IPosgresqlConfig
{
    public string ConnectionString { get; set; }
}