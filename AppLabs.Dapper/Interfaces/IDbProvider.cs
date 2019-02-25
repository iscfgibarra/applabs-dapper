using System.Data;

namespace AppLabs.Dapper.Interfaces
{
    public interface IDbProvider
    {
        string ProviderName { get; set; }
        ProviderType ProviderType { get; set; }
        string GetConnectionString();
        IDbConnection GetConnection();
    }
}
