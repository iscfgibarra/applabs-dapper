using System.Data;
using System.Data.SqlClient;
using AppLabs.Dapper.Abstractions;
using AppLabs.QueryExpression;

namespace AppLabs.Dapper.Test.Providers
{
    public class SqlProvider : IDbProvider
    {
        private DbProviderType _providerType;

        public SqlProvider()
        {
            ProviderName = "SqlLogConexion";
            ProviderType = DbProviderType.SqlServer;
        }
        public string ProviderName { get; set; }

        DbProviderType IDbProvider.ProviderType
        {
            get => _providerType;
            set => _providerType = value;
        }

        public DbProviderType ProviderType { get; set; }
        public string GetConnectionString()
        {
            return "Data Source=LOCALHOST\\STAMPING;Initial Catalog=DBLOG;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public DbTypes GetDbType => DbTypes.MsSql;
    }
}
