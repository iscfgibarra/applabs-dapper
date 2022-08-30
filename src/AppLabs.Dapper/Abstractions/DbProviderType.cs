using System.ComponentModel;

namespace AppLabs.Dapper.Abstractions;

public enum DbProviderType
{
    None,
    [Description("mssql")]
    SqlServer,
    [Description("mysql")]
    MySql,
    [Description("postgres")]
    Postgres,
    Sqlite,
    Oracle
}