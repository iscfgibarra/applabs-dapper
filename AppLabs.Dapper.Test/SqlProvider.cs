using System.Data;
using System.Data.SqlClient;
using AppLabs.Dapper.Interfaces;

namespace AppLabs.Dapper.Test
{
    public class SqlProvider : IDbProvider
    {
        public SqlProvider()
        {
            ProviderName = "SqlLogConexion";
            ProviderType = ProviderType.SqlServer;
        }
        public string ProviderName { get; set; }
        public ProviderType ProviderType { get; set; }
        public string GetConnectionString()
        {
            return "Data Source=LOCALHOST\\STAMPING;Initial Catalog=DBLOG;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}
